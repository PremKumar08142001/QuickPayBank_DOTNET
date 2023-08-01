import { Component, OnInit } from '@angular/core';
import { MatTableDataSource } from '@angular/material/table';
import { ActivatedRoute } from '@angular/router';
import { FundTransferService } from '../services/fund-transfer.service';
import { AuthenticationService } from '../services/authentication.service';
import { AccountService } from '../services/account.service';

@Component({
  selector: 'app-transactions',
  templateUrl: './transactions.component.html',
  styleUrls: ['./transactions.component.css']
})
export class TransactionsComponent implements OnInit{

  branch:any;
  page = 1;
	pageSize = 4;
	transcationlist:any;
  transcation:any;
  collectionSize:any;
  accountDetails:any;
  transactions: any[] = [  
      // {
      //     "id": 2,
      //     "amount": 3400,
      //     "senderAccountNumber": "1",
      //     "receiverAccountNumber": "4246342136",
      //     "transactionDate": "2023-06-29T12:40:37.3496744",
      //     "status": "Pending",
      //     "userId": 123
      // },
      // {
      //     "id": 3,
      //     "amount": 5000,
      //     "senderAccountNumber": "1",
      //     "receiverAccountNumber": "4246342136",
      //     "transactionDate": "2023-06-29T13:43:43.6935396",
      //     "status": "Suspicious",
      //     "userId": 123
      // }
  ];

  displayedColumns: any[] = ['id', 'transactionDate', 'recipient', 'status', 'amount'];

  // dataSource = new MatTableDataSource<any>(this.transactions);
  userExist:any;
  dataSource: any
  userDetails:any
  userId: any
  username:any
  getStatusClass(status: string): string {
    if (status === 'Suspicious') {
      return 'status-suspicious';
    } else if (status === 'Completed') {
      return 'status-complete';
    } else {
      return '';
    }
  }

  constructor(private transactionService: FundTransferService,private accountService:AccountService,
    private route: ActivatedRoute,private authService:AuthenticationService) {
  }
  
  ngOnInit() {
    
    //this.userId = localStorage.getItem("userId");

    // this.transactionService.getTransactionsByUserId(this.userId).subscribe(data => {

    // })
 
    this.username = this.route.snapshot.paramMap.get('userName');
    

  this.accountService.accountExist(this.username).subscribe(res=>{this.userExist=res;
    console.log(this.userExist);
    if(this.userExist){
      this.accountService.accountByName(this.username).subscribe(res=>{this.accountDetails=res;
       
      });
    }
  }
    );
    this.authService.getUserDetails(this.username).subscribe(resp=>{
      
      console.log(this.username,"jk..........................")
      this.userDetails=resp;
     
    
    this.transactionService.getTransaction().subscribe(data => {
      
      console.log(".....",this.userId,"................");
      this.transactions = data.filter((transaction: { userId: any; }) => transaction.userId == this.userDetails.userId);

      this.transactions = this.transactions.map(transaction => {
        const formattedDate = new Date(transaction.transactionDate);
     
        return {
          ...transaction,
          transactionDate: formattedDate.toLocaleString('en-US', { year: 'numeric', month: 'short', day: 'numeric', hour: 'numeric', minute: 'numeric', hour12: true })
        };
      });

     // this.dataSource = new MatTableDataSource<any>(this.transactions);
      this.transcationlist=[...this.transactions];
      this.collectionSize=this.transcationlist.length

      this.refreshCountries() 
      console.log('utransactions', this.transactions, data);
    })})
  }
  refreshCountries() {
    this.transcation = this.transcationlist.map((accountlist: any, i: number) => ({ index: i + 1, ...accountlist })).slice(
      (this.page - 1) * this.pageSize,
      (this.page - 1) * this.pageSize + this.pageSize,
    );
  }
}
