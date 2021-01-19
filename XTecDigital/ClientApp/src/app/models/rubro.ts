import { Evaluacion } from "./evaluacion";

export class Rubro {
    nombre: string;
    porcentaje: number;
    numero: number;
    curso: string;
    anio: number;
    periodo: string;
    evaluaciones: Evaluacion[];

    constructor() { }
}