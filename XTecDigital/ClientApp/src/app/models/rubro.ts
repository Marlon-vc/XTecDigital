import { Evaluacion } from "./evaluacion";

export class Rubro {
    id: number;
    idGrupo: number;
    nombre: string;
    porcentaje: number;
    evaluaciones: Evaluacion[];
    
    //agregar evaluaciones

    constructor() { }
}