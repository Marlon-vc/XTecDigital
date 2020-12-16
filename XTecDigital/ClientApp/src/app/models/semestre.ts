import { Grupo } from "./grupo";

export class Semestre {
    id: number;
    anio: number;
    periodo: string;
    grupos: Grupo[];

    constructor() { }
}