import { HttpClient } from '@angular/common/http';
import { Injectable, OnInit } from '@angular/core';
import { loanApplied } from '../models/loanApplied';
import configurl from '../../assets/config/config.json';
@Injectable({
  providedIn: 'root'
})
export class LoanAppliedService implements OnInit{
  private url = configurl.apiServer.url;

  constructor(private httpClient : HttpClient) { }

  getAllAppliedLoans(){
    return this.httpClient.get<loanApplied[]>(this.url+"/LoansApplied");
  }
  addAppliedLoans(data:loanApplied){
    console.log("....api called",data)
 return this.httpClient.post(this.url+"/LoansApplied",data);
    
  }
  AppliedLoansbyname(data:string){

 return this.httpClient.get(this.url+"/loan/"+data);
    
  }
  updateBalance(accountnumber:any,balance:any) {

    return this.httpClient.get(this.url+"/updateloan?accountNumber="+accountnumber+"&balance="+balance)
  }
  ngOnInit(): void {
    
  }

}
