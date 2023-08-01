import { Component } from '@angular/core';
import { Subscription, count, interval } from 'rxjs';
import { FundTransferService } from '../services/fund-transfer.service';

@Component({
  selector: 'app-recent-activity',
  templateUrl: './recent-activity.component.html',
  styleUrls: ['./recent-activity.component.css']
})
export class RecentActivityComponent {

  totalTransactions!: number

  suspiciousTransactions!: number

  subscription!: Subscription;

  _tdata!: any[]

  data!: any[]

  constructor(private transactionService: FundTransferService) {}

  ngOnInit() {

    this.subscription = interval(1000).subscribe(() => {
      const newCount = this.getTransactionCount();
      console.log('transactionCount ', newCount);
      this.transactionService.updateTransactionCount(newCount);
    });

    this.subscription.add(
      this.transactionService.transactionCount$.subscribe(newCount => {
        this.totalTransactions = newCount;
      })
    );

  }

  getTransactionCount(): number {
    // this.transactionService.getTransaction().subscribe(transactions => {
    //   console.log('totaltransactions ', transactions.length);
    //   this.totalTransactions = transactions.length
    //   this.suspiciousTransactions = (transactions.filter((transactions: { status: any; }) => transactions.status == 'Blocked')).length
    //   return transactions;  
    // })
    this.transactionService.getTransaction().subscribe(transactions => {
      this.totalTransactions = transactions.length
      this.suspiciousTransactions = (transactions.filter((transactions: { status: any; }) => transactions.status == 'Suspicious')).length
      return transactions.length
    })
    return this.totalTransactions
  }

}
