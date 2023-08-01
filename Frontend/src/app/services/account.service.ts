import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import configurl from '../../assets/config/config.json';
import { Observable } from 'rxjs';
import { map } from 'rxjs';
@Injectable({
  providedIn: 'root'
})
export class AccountService {
  private temp:any;
  private authUrl = configurl.apiServer.url;
  constructor(private httpClient: HttpClient) {
    
  }
  getAccountByUserId(userId: any): Observable<any> {
    return this.httpClient.get<any>(this.authUrl+"/Account/byId/"+userId)
  }
  accountExist(data:string) {
    return this.httpClient.get<boolean>(this.authUrl+"/accountExist/"+data)
  
  }
  accountByName(data:string) {
    return this.httpClient.get<boolean>(this.authUrl+"/accounbyName/"+data)
  
  }
  addAccount(data:any) {
    console.log(".....aaaaa....",data,".........")
    return this.httpClient.post(this.authUrl+"/Account",data)
  }
  getAccounts(data:any) {

    return this.httpClient.get(this.authUrl+"/byBranchcode/"+data)
  }

  updateBalance(accountnumber:any,balance:any) {

    return this.httpClient.get(this.authUrl+"/updateBalance?accountNumber="+accountnumber+"&balance="+balance)
  }
  //https://localhost:7013/updateBalance?accountNumber=2008908080808&balance=2000
  //https://localhost:7013/updateBalance?accountNumber=2008908080808&balance=-7666
  //   console.log(data);
  //   return this.httpClient.get(this.authUrl+"/accountExist/"+data);
  // }
}
