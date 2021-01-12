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

        [HttpGet("root/{groupId}")]
        public async Task<IActionResult> GetAllAsync(int groupId)
        {
            var folders = await _context.Carpeta.FromSqlInterpolated($@"
                dbo.sp_get_group_folders {groupId}
            ").ToListAsync();

            var files = await _context.Archivo.FromSqlInterpolated($@"
                dbo.sp_get_group_files {groupId}
            ").ToListAsync();

            var rootFolder = (await _context.Carpeta.FromSqlInterpolated($@"
                dbo.sp_get_root_folder {groupId}
            ").ToListAsync()).FirstOrDefault();

            if (rootFolder == null)
                return BadRequest();

            var data = new {
                files = _mapper.Map<List<ArchivoDto>>(files),
                folders = _mapper.Map<List<CarpetaDto>>(folders),
                root = _mapper.Map<CarpetaDto>(rootFolder)
            };

            return Ok(data);
        }

        [HttpGet("folder/{folderId}")]
        public async Task<IActionResult> GetFolderContentsAsync(int folderId)
        {
            var files = await _context.Archivo.FromSqlInterpolated($@"
                dbo.sp_get_files {folderId}
            ").ToListAsync();

            return Ok(_mapper.Map<List<ArchivoDto>>(files));
        }

        [HttpGet("file/{fileId}")]
        public async Task<IActionResult> GetFileAsync(int fileId)
        {
            var file = _context.Archivo.FromSqlInterpolated($@"
                dbo.sp_get_file {fileId}
            ").AsEnumerable()
            .FirstOrDefault();

            var folder = _context.Carpeta.FromSqlInterpolated($@"
                dbo.sp_get_folder {file.IdCarpeta}
            ").AsEnumerable()
            .FirstOrDefault();
            
            if (file == null || folder == null)
                return NotFound();

            //DEBUG: probar que se descargue correctamente
            var groupFolder = Path.Combine(Environment.CurrentDirectory, "Storage", $"Grupo-{folder.IdGrupo}");
            var filePath = Path.Combine(groupFolder, "Documentos", file.Nombre);
            var contents = await System.IO.File.ReadAllBytesAsync(filePath);

            // return File(contents, "application/force-download", file.Nombre);
            return File(contents, "application/octet-stream", file.Nombre);
        }

        [HttpPost("file")]
        public async Task<IActionResult> CreateFileAsync(UploadInfo info)
        {
            if (string.IsNullOrWhiteSpace(info.FileData))
                return BadRequest();

            var fileName = info.Name.CoerceValidFileName();

            //TODO: almacenar el archivo en filesystem
            var data = FileHandler.FromBase64String(info.FileData);

            var path = Path.Combine(FileHandler.StoragePath, $"Grupo-{info.GroupId}", "Documentos", fileName);

            await System.IO.File.WriteAllBytesAsync(path, data);

            await _context.Database.ExecuteSqlInterpolatedAsync($@"
                dbo.sp_create_file {info.FolderId}, {fileName}, {DateTime.Now}, {info.Size}
            ");

            var file = (await _context.Archivo.FromSqlInterpolated($@"
                dbo.sp_get_file_from_name {info.FolderId}, {fileName} 
            ").ToListAsync()).FirstOrDefault();

            return CreatedAtRoute("Default", new { id = file.Id }, file);
        }

        [HttpPost("folder")]
        public async Task<IActionResult> CreateFolderAsync(FolderCreation info)
        {
            if (string.IsNullOrWhiteSpace(info.Name))
                return BadRequest();
            
            await _context.Database.ExecuteSqlInterpolatedAsync($@"
                dbo.sp_create_folder {info.GroupId}, {info.Name}, {0}, {0}
            ");

            var folder = (await _context.Carpeta.FromSqlInterpolated($@"
                dbo.sp_get_folder_by_name {info.GroupId}, {info.Name}
            ").ToListAsync()).FirstOrDefault();

            return CreatedAtRoute("Default", new { id = folder.Id }, folder);
        }

        [HttpDelete("file/{fileId}")]
        public async Task<IActionResult> DeleteFileAsync(int fileId)
        {
            var file = (await _context.Archivo.FromSqlInterpolated($@"
                dbo.sp_get_file {fileId}
            ").ToListAsync()).FirstOrDefault();

            if (file == null)
                return BadRequest();

            var folder = (await _context.Carpeta.FromSqlInterpolated($@"
                dbo.sp_get_folder {file.IdCarpeta}
            ").ToListAsync()).FirstOrDefault();

            if (folder == null)
                return BadRequest();

            await _context.Database.ExecuteSqlInterpolatedAsync($@"
                dbo.sp_delete_file {fileId}
            ");

            var path = Path.Combine(FileHandler.StoragePath, $"Grupo-{folder.IdGrupo}", "Documentos", file.Nombre);

            System.IO.File.Delete(path);

            return Ok();
        }

        [HttpDelete("folder/{folderId}")]
        public async Task<IActionResult> DeleteFolderAsync(int folderId)
        {
            var files = await _context.Archivo.FromSqlInterpolated($@"
                dbo.sp_get_files {folderId}
            ").ToListAsync();

            foreach (var file in files)
            {
                await DeleteFileAsync(file.Id);
            }

            await _context.Database.ExecuteSqlInterpolatedAsync($@"
                dbo.sp_delete_folder {folderId}
            ");

            return Ok();
        }

        // [HttpPost("test")]
        // public IActionResult TestMethod([FromForm] TestInfo data)
        // {
        //     Console.WriteLine(data.GroupId);
        //     Console.WriteLine(data.FolderId);

        //     return Ok(data);
        // }
    }
}