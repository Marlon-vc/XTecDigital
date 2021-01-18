
export class SessionHandler {

    /**
     * Metodo para verificar si la sesion esta iniciada
     */
    static isLoggedIn() {
        var userId = localStorage.getItem('user-id');
        var userType = localStorage.getItem('user-type');
        return userId != null && userType != null;
    }

    /**
     * Metodo para setear los datos del usuario logueado
     * @param userId Id del usuario
     * @param type Tipo de usuario
     */
    static logIn(userId: string, type: string) {
        localStorage.setItem('user-id', userId);
        localStorage.setItem('user-type', type);
    }

    /**
     * Metodo para obtener el tipo de usuario logueado
     */
    static getUserType() {
        return localStorage.getItem('user-type');
    }

    /**
     * Metodo para obtener el id del usuario logueado
     */
    static getUserId() {
        return localStorage.getItem('user-id');
    }

}