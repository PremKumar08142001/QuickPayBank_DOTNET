import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { FundTransferService } from '../services/fund-transfer.service';
import { AccountService } from '../services/account.service';

@Component({
  selector: 'app-unusual-transaction-alerts',
  templateUrl: './unusual-transaction-alerts.component.html',
  styleUrls: ['./unusual-transaction-alerts.component.css']
})
export class UnusualTransactionAlertComponent implements OnInit {

  suspiciousTransactions!: { key: string, value: any }
  
  userName: any;

  constructor(private transactionService: FundTransferService, private accountService:AccountService ,private router: Router) {}

  ngOnInit() {
    this.transactionService.getSuspiciousTransaction().subscribe((data: any) => {
      console.log('suspicious ......................', data);
      this.suspiciousTransactions = this.groupTransactions(data)
      console.log('groupedSuspiciousTransactions', this.suspiciousTransactions);
    })
  }

  getUserName(userId: any): any {
    this.accountService.getAccountByUserId(userId).subscribe((data: { userName: any; }) => {
        this.userName = data.userName
        console.log('getuserName ', data, this.userName);
      })
      return this.userName
    }

  groupTransactions(transactions: any) {
    const groupedTransactions = transactions.reduce((acc: any, transaction: any) => {
      const { userId, ...rest } = transaction;

      //const userName = this.getUserName(userId);
      this.accountService.getAccountByUserId(userId).subscribe((data: { userName: any; }) => {
        
        const _userName = data.userName
        console.log('userName ................', this.userName);
        console.log('data ................', data);
        
        if (!acc[userId]) {
          acc[userId] = { _userName, transactions: [], count: 0 };
        }
        acc[userId].transactions.push(rest);
        acc[userId].count++;
      })
      console.log('groupedSuspiciousTransactions', acc);
      return acc;
    }, {});
    return groupedTransactions;
  } 
  

  
  viewCustomerDetails(userId: any) {
    console.log('View customer details', userId);
  }

  viewCustomerTransactions(userId: any) {
    console.log('View customer transactions ', userId);

    this.router.navigate(['/transactions', userId]);

  }
  handleTransactions(userId: any) {

    console.log('View customer transactions ', userId);

 

    this.router.navigate(['/profile', userId]);
}
}
