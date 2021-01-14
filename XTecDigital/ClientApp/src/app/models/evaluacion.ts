import { InfoEvaluacion } from "./infoEvaluacion";

export class Evaluacion {

    id: number;
    idRubro: number;
    idEspecificacion: number;
    nombre: string;
    notasPublicadas: boolean;
    pesoNota: number;
    grupal: boolean;
    fechaEntrega: string;
    info: InfoEvaluacion;

    constructor() { }
}