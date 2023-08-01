import { Injectable } from '@angular/core';
import configurl from '../../assets/config/config.json';
import { BehaviorSubject, Observable, Subject } from 'rxjs';
import { map } from 'rxjs';
import { HttpClient } from '@angular/common/http';
import { FundTransfer } from '../models/FundTransfer';
@Injectable({
  providedIn: 'root'
})
export class FundTransferService {
  private authUrl = configurl.apiServer.url;
  constructor(private httpClient: HttpClient) {
    
  }
  getTransaction(): Observable<any>  {
    return this.httpClient.get<FundTransfer[]>(this.authUrl + '/Transaction');
  }
  addTransfer(addTransferRequest: FundTransfer): Observable<FundTransfer>
  {
    console.log(addTransferRequest,"serice")
     return this.httpClient.post<FundTransfer>(this.authUrl + '/Transaction',addTransferRequest)
  }
  
  getSuspiciousTransaction(): Observable<any> {
    return this.httpClient.get<any>(this.authUrl + "/Transaction/suspicious")
  }
  private transactionDataSubject = new Subject<any[]>();

  transactionData$ = this.transactionDataSubject.asObservable();

  updateTransactionData(transactions: any[]) {
    this.transactionDataSubject.next(transactions);
  }

  private transactionCountSubject = new BehaviorSubject<number>(0);
  
  transactionCount$ = this.transactionCountSubject.asObservable();

  updateTransactionCount(count: number) {
    this.transactionCountSubject.next(count);
  }

}
