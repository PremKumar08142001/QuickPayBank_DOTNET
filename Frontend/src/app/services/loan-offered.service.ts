import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { loanOffered } from '../models/loanOffered';
import configurl from '../../assets/config/config.json';

@Injectable({
  providedIn: 'root'
})
export class LoanOfferedService {
  private url = configurl.apiServer.url;

  constructor(private httpClient : HttpClient) { }

 getloansOffered():Observable<loanOffered[]>
 {
  return this.httpClient.get<loanOffered[]>(this.url+"/LoansOffered1");
 }
 addLoan(data:any)
 {
  console.log(data)
  return this.httpClient.post(this.url+"/LoansOffered1",data);
 }

 
}
