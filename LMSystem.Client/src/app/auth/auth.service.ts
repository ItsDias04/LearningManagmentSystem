import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { catchError, tap, throwError } from 'rxjs';
import { TokenResponse } from '../auth/auth.interface';
import { CookieService } from 'ngx-cookie-service';
import { Router } from '@angular/router';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  token: TokenResponse | null = null;
  role: string | null = null;
  cookieService = inject(CookieService);
  http: HttpClient = inject(HttpClient);
  router = inject(Router);
  baseApiUrl = 'https://localhost:7186/api';
  get Role() {
    return this.cookieService.get('role');
  }

  get isAuth() {
    if (!this.token) {
      const token = this.cookieService.get('token');

      if (token) {
        this.token = { token: { token, expiration: '' }, role: this.cookieService.get('role') };
      }
    }
    return !!this.token;
  }

  roleAccess(allowedRoles: string[]) { 
    if (!this.isAuth) {
      return false;
    }
    const role = this.cookieService.get('role');
    if (!role) {
      return false;
    }

    if (!allowedRoles.includes(this.token!.role) || !allowedRoles.includes(role))  {
      return false;
    }
    return true;
  }

  login(payload: { username: string; password: string }) {
    return this.http.post<TokenResponse>(`${this.baseApiUrl}/Auth/login`, payload).pipe(
      tap((res) => {
        this.token = res;
        this.cookieService.set('token', res.token.token);
        this.cookieService.set('role', res.role);
      }),
      catchError((error) => {
        console.error('Login failed', error);
        return throwError(() => new Error('Login failed'));
      })
    );
  }

  logout() {
    this.token = null;
    this.cookieService.deleteAll();
    this.router.navigate(['/login']);
  }
}
