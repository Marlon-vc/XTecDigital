
export class SessionHandler {

    static isLoggedIn() {
        var userId = localStorage.getItem('user-id');
        var userType = localStorage.getItem('user-type');
        return userId != null && userType && null;
    }

    static logIn(userId: string, type: string) {
        localStorage.setItem('user-id', userId);
        localStorage.setItem('user-type', type);
    }

    static getUserType() {
        return localStorage.getItem('user-type');
    }

    static getUserId() {
        return localStorage.getItem('user-id');
    }

}