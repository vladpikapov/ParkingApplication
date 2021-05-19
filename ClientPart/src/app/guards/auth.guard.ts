import {Injectable} from '@angular/core';
import {CanActivate, Router} from '@angular/router';
import {AuthService} from '../services/auth.service';

@Injectable({
  providedIn: 'root'
})
export class AuthGuard implements CanActivate {
  canActivate(): boolean {
    if (!this.as.isAuthenticated()) {
      // this.router.navigate(['/login']);
    }
    return true;
  }

  constructor(private as: AuthService, private router: Router) {
  }
}
