import { Injectable } from '@angular/core';
import { CanActivate, ActivatedRouteSnapshot, RouterStateSnapshot, Router } from '@angular/router';
import { JwtHelperService } from '@auth0/angular-jwt';

@Injectable()
export class AuthGuard implements CanActivate {
  constructor(
   
  ) {}

  canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): boolean {
    const token = localStorage.getItem('bearerToken');
    const role = localStorage.getItem('role')
    if (token && role=="3") {
      // Token is valid and not expired, allow access to the route
      return true;
    } else {
      // Token is invalid or expired, redirect to login page or any other desired route
    
      return false;
    }
  }
}
