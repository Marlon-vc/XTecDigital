using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using XTecDigital.Helpers;
using XTecDigital.Models;
using XTecDigital.Models.Dtos;
using XTecDigital.Models.Requests;
using System.Linq;
using System.IO;

namespace XTecDigital.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ArchivosController: ControllerBase
    {
        public readonly AppDbContext _context;
        public readonly IMapper _mapper;

        public ArchivosController(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet("root")]
        public async Task<IActionResult> GetAllAsync([FromQuery] GrupoDto grupo)
        {
            var folders = await _context.Carpeta.FromSqlInterpolated($@"
                dbo.sp_get_group_folders {grupo.Numero}, {grupo.Curso}, {grupo.Anio}, {grupo.Periodo}
            ").ToListAsync();

            var files = await _context.Archivo.FromSqlInterpolated($@"
                dbo.sp_get_group_files {grupo.Numero}, {grupo.Curso}, {grupo.Anio}, {grupo.Periodo}
            ").ToListAsync();

            var rootFolder = (await _context.Carpeta.FromSqlInterpolated($@"
                dbo.sp_get_root_folder {grupo.Numero}, {grupo.Curso}, {grupo.Anio}, {grupo.Periodo}
            ").ToListAsync()).FirstOrDefault();

            if (rootFolder == null)
                return NotFound();

            var data = new {
                files = _mapper.Map<List<ArchivoDto>>(files),
                folders = _mapper.Map<List<CarpetaDto>>(folders),
                root = _mapper.Map<CarpetaDto>(rootFolder)
            };

            return Ok(data);
        }

        [HttpGet("contents")]
        public async Task<IActionResult> GetFolderContentsAsync([FromQuery] CarpetaDto carpeta)
        {
            var files = await _context.Archivo.FromSqlInterpolated($@"
                dbo.sp_get_files {carpeta.Nombre}, {carpeta.Tipo}, {carpeta.Numero}, {carpeta.Curso}, {carpeta.Anio}, {carpeta.Periodo}
            ").ToListAsync();

            return Ok(_mapper.Map<List<ArchivoDto>>(files));
        }

        [HttpGet("download")]
        public async Task<IActionResult> GetFileAsync([FromQuery] ArchivoDto archivo)
        {
            var file = (await _context.Archivo.FromSqlInterpolated($@"
                dbo.sp_get_file {archivo.Nombre}, {archivo.Carpeta}, {archivo.TipoCarpeta}, {archivo.Numero}, {archivo.Curso}, {archivo.Anio}, {archivo.Periodo}
            ").ToListAsync()).FirstOrDefault();

            if (file == null)
                return NotFound();

            // Carpeta general del grupo
            var groupFolder = FileHandler.GetGroupFolder(file.Numero, file.Curso, file.Anio, file.Periodo);

            string folder;
            if (file.TipoCarpeta.Equals("NORMAL"))
            {
                var rootFolder = (await _context.Carpeta.FromSqlInterpolated($@"
                    dbo.sp_get_root_folder {file.Numero}, {file.Curso}, {file.Anio}, {file.Periodo}
                ").ToListAsync()).FirstOrDefault();

                if (rootFolder == null)
                    return NotFound();

                //DEBUG: si es tipo normal está dentro de la carpeta Documentos del grupo
                folder = Path.Combine(rootFolder.Nombre, file.Carpeta);
            } else
            {
                folder = file.Carpeta;
            }
            
            var filePath = Path.Combine(groupFolder, folder, file.Nombre);
            var contents = await System.IO.File.ReadAllBytesAsync(filePath);

            return File(contents, "application/octet-stream", file.Nombre);
        }

        [HttpPost("file")]
        public async Task<IActionResult> CreateFileAsync(UploadInfo info)
        {
            if (string.IsNullOrWhiteSpace(info.FileData))
                return BadRequest();

            var fileName = info.Name.CoerceValidFileName();

            var data = FileHandler.FromBase64String(info.FileData);

            var groupFolder = FileHandler.GetGroupFolder(info.Numero, info.Curso, info.Anio, info.Periodo);

            string folder;

            if (info.TipoCarpeta.Equals("NORMAL"))
            {
                var rootFolder = (await _context.Carpeta.FromSqlInterpolated($@"
                    dbo.sp_get_root_folder {info.Numero}, {info.Curso}, {info.Anio}, {info.Periodo}
                ").ToListAsync()).FirstOrDefault();

                if (rootFolder == null)
                    return NotFound();

                folder = Path.Combine(rootFolder.Nombre, info.Carpeta);
            } else
            {
                folder = info.Carpeta;
            }

            var filePath = Path.Combine(groupFolder, folder, fileName);
            await System.IO.File.WriteAllBytesAsync(filePath, data);

            await _context.Database.ExecuteSqlInterpolatedAsync($@"
                dbo.sp_create_file {fileName}, {DateTime.Now}, {info.Size}, {info.Carpeta}, {info.TipoCarpeta}, {info.Numero}, {info.Curso}, {info.Anio}, {info.Periodo}
            ");

            // var file = (await _context.Archivo.FromSqlInterpolated($@"
            //     dbo.sp_get_file_from_name {info.FolderId}, {fileName}
            // ").ToListAsync()).FirstOrDefault();

            var file = (await _context.Archivo.FromSqlInterpolated($@"
                dbo.sp_get_file {fileName}, {info.Carpeta}, {info.TipoCarpeta}, {info.Numero}, {info.Curso}, {info.Anio}, {info.Periodo}
            ").ToListAsync()).FirstOrDefault();

            return CreatedAtRoute("Default", file);
        }

        // Solo se pueden crear carpetas dentro de los documentos del grupo 
        [HttpPost("folder")]
        public async Task<IActionResult> CreateFolderAsync(FolderCreation info)
        {
            if (string.IsNullOrWhiteSpace(info.Nombre))
                return BadRequest();

            await _context.Database.ExecuteSqlInterpolatedAsync($@"
                dbo.sp_create_folder {info.Nombre}, {0}, {"NORMAL"}, {info.Numero}, {info.Curso}, {info.Anio}, {info.Periodo}
            ");

            var folder = (await _context.Carpeta.FromSqlInterpolated($@"
                dbo.sp_get_folder {info.Nombre}, {"NORMAL"}, {info.Numero}, {info.Curso}, {info.Anio}, {info.Periodo}
            ").ToListAsync()).FirstOrDefault();

            return CreatedAtRoute("Default", folder);
        }

        [HttpDelete("file")]
        public async Task<IActionResult> DeleteFileAsync([FromQuery] ArchivoDto archivo)
        {
            await _context.Database.ExecuteSqlInterpolatedAsync($@"
                dbo.sp_delete_file {archivo.Nombre}, {archivo.Carpeta}, {archivo.TipoCarpeta}, {archivo.Numero}, {archivo.Curso}, {archivo.Anio}, {archivo.Periodo}
            ");

            var groupFolder = FileHandler.GetGroupFolder(archivo.Numero, archivo.Curso, archivo.Anio, archivo.Periodo);

            string folder;

            if (archivo.TipoCarpeta == "NORMAL")
            {
                var rootFolder = (await _context.Carpeta.FromSqlInterpolated($@"
                    dbo.sp_get_root_folder {archivo.Numero}, {archivo.Curso}, {archivo.Anio}, {archivo.Periodo}
                ").ToListAsync()).FirstOrDefault();

                if (rootFolder == null)
                    return NotFound();

                folder = Path.Combine(rootFolder.Nombre, archivo.Carpeta);
            } else
            {
                folder = archivo.Carpeta;
            }

            // var path = Path.Combine(FileHandler.StoragePath, $"Grupo-{folder.IdGrupo}", "Documentos", file.Nombre);

            System.IO.File.Delete(Path.Combine(groupFolder, folder, archivo.Nombre));

            return Ok();
        }

        [HttpDelete("folder")]
        public async Task<IActionResult> DeleteFolderAsync([FromQuery] CarpetaDto carpeta)
        {
            if (!carpeta.Tipo.Equals("NORMAL"))
                return BadRequest();

            var rows = await _context.Database.ExecuteSqlInterpolatedAsync($@"
                dbo.sp_delete_folder {carpeta.Nombre}, {carpeta.Tipo}, {carpeta.Numero}, {carpeta.Curso}, {carpeta.Anio}, {carpeta.Periodo}
            ");

            if (rows == 0)
                return NotFound();

            var groupFolder = FileHandler.GetGroupFolder(carpeta.Numero, carpeta.Curso, carpeta.Anio, carpeta.Periodo);

            var rootFolder = (await _context.Carpeta.FromSqlInterpolated($@"
                dbo.sp_get_root_folder {carpeta.Numero}, {carpeta.Curso}, {carpeta.Anio}, {carpeta.Periodo}
            ").ToListAsync()).FirstOrDefault();

            if (rootFolder == null)
                return NotFound();

            var path = Path.Combine(groupFolder, rootFolder.Nombre, carpeta.Nombre);
            
            if (Directory.Exists(path))
            {
                Directory.Delete(path, true);
            } else 
            {
                return NotFound();
            }
            
            return Ok();
        }
    }
}