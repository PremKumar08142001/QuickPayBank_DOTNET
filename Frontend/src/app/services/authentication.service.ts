import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import configurl from '../../assets/config/config.json';
import { JwtHelperService } from '@auth0/angular-jwt';
import { Observable, map } from 'rxjs';
@Injectable({
  providedIn: 'root'
})
export class AuthenticationService {
  private temp:any;
  private authUrl = configurl.apiServer.url;
  constructor(private httpClient: HttpClient) {
    
  }

  authenticateUser(data: any) {

    return this.httpClient.post(this.authUrl+"/login", data);
  }

  setBearerToken(token: string) {
    localStorage.setItem('bearerToken', token);
  }
  decodedToken(){
    const jwtHelper = new JwtHelperService();
    const token = this.getBearerToken()!;
   // console.log(jwtHelper.decodeToken(token))
    return jwtHelper.decodeToken(token)
  }
setClames(){
  this.temp=this.decodedToken();
  // console.log(this.temp);
  // console.log(this.temp["http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name"])
console.log(this.temp["http://schemas.microsoft.com/ws/2008/06/identity/claims/role"])
  localStorage.setItem('name', this.temp["http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name"]);
  localStorage.setItem('role', this.temp["http://schemas.microsoft.com/ws/2008/06/identity/claims/role"].length);
  return this.temp["http://schemas.microsoft.com/ws/2008/06/identity/claims/role"].length;
}

  getBearerToken() {
    return localStorage.getItem('bearerToken');
  }
  registerUser(data: any) {
    console.log(data,"..................")
    return this.httpClient.post(this.authUrl+"/register", data);
  }
  registerManager(data: any) {
    return this.httpClient.post(this.authUrl+"/register-manager", data);
  }
  registerAdmin(data: any) {
    return this.httpClient.post(this.authUrl+"/register-admin", data);
  }
  getUserDetails(data: string) : Observable<any>{
    return this.httpClient.get(this.authUrl+"/userByName/"+data);
  }
  getAllManagers() {
    return this.httpClient.get(this.authUrl+"/getManagers");
  }
}