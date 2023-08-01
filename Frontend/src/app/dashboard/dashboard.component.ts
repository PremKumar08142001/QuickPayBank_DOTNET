import { Component, OnInit } from '@angular/core';
import { AccountService } from '../services/account.service';
import { BranchServiceService } from '../services/branch-service.service';
import { Account } from '../models/account';
import { NgForm } from '@angular/forms';
import { AuthenticationService } from '../services/authentication.service';
import { FundTransferService } from '../services/fund-transfer.service';
import { LoanAppliedService } from '../services/loan-applied.service';
import { FundTransfer } from '../models/FundTransfer';
import { ToastrService } from 'ngx-toastr';
import { Router } from '@angular/router';

@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.css']
})
export class DashboardComponent implements OnInit{
  addTransferRequest:any
  loanpaid:any;
  branch:any;
  page = 1;
	pageSize = 8;
	transcationlist:any;
  transcation:any;
  collectionSize:any;

userExist:any;

userName?:any="";
branchlist?:any;
baranchCode:any;
accou:Account=new Account();
temp!:any;
accountDetails!:any;
userforID!:any;
loansofuser:any;
constructor(private accountService:AccountService,private branchservice:BranchServiceService,private authService: AuthenticationService,private transactionService:FundTransferService,private loanservice:LoanAppliedService,private toastr: ToastrService,private router:Router){
 
}
loanamount(amount:number,terner:number,interest:number){
  
  return Math.ceil(((amount*terner*interest/100)+amount)/(terner*12))
}
ngOnInit(): void {
  // this.branchService.getBranchByName(localStorage.getItem("name")).subscribe(resp=>{this.branch=resp;
  //   this.accountService.getAccounts(this.branch.branchCode).subscribe(resp=>{this.accountlist=resp;
  //     this.collectionSize=this.accountlist.length;
  //     this.refreshCountries()
  //   });});
  
  this.branchservice.getAllBranches().subscribe(res=>{this.branchlist=res;console.log(this.branchlist);})
 
  this.userName=localStorage.getItem('name');
  console.log(this.userName);
  this.accountService.accountExist(this.userName).subscribe(res=>{this.userExist=res;
    console.log(this.userExist);
    if(this.userExist){
      this.accountService.accountByName(this.userName).subscribe(res=>{this.accountDetails=res;
        this.getTransactionsHistory(this.accountDetails.accountNumber)
      });
    }
  }
    );
    this.authService.getUserDetails(this.userName).subscribe(resp=>{
      this.userforID=resp;
      console.log(this.userforID)
        localStorage.setItem("userId",this.userforID.userId);
    });
    
    this.loanservice.AppliedLoansbyname(this.userName).subscribe(res=>{this.loansofuser=res;
    console.log(this.loansofuser,".................")
    });
}
public onOpenModal(): void {
  console.log("openmodule")
  const container = document.getElementById('page-content-wrapper');
  const button = document.createElement('button');
  button.type = 'button';
  button.style.display = 'none';
  button.setAttribute('data-bs-toggle', 'modal');
  button.setAttribute('data-bs-target', '#updateUserModal');
  container?.appendChild(button);
  button.click();
  
}
public openloan(): void {
  console.log("openmodule")
  const container = document.getElementById('page-content-wrapper');
  const button = document.createElement('button');
  button.type = 'button';
  button.style.display = 'none';
  button.setAttribute('data-bs-toggle', 'modal');
  button.setAttribute('data-bs-target', '#loan');
  container?.appendChild(button);
  button.click();
  
}
public onCreate():void{
  
  console.log(this.baranchCode.split("-"),".........")
  
  this.accou.userName=this.userName;
  this.accou.branchCode=this.baranchCode.split("-")[1];
  this.accou.branchName=this.baranchCode.split("-")[0];
 this.accou.userId=this.userforID.userId;
 
  this.accountService.addAccount(this.accou).subscribe(res=>console.log("jkjkkk",res));
}
refreshCountries() {
  this.transcation = this.transcationlist.map((accountlist: any, i: number) => ({ index: i + 1, ...accountlist })).slice(
    (this.page - 1) * this.pageSize,
    (this.page - 1) * this.pageSize + this.pageSize,
  );
}
transactions: any
 displayedColumns: any[] = ['id', 'transactionDate', 'recipient', 'amount'];
  dataSource: any
  userId: any
  getTransactionsHistory(accountNumber:string) {
    console.log('userId', this.userId);
    this.transactionService.getTransaction().subscribe(data => {
      this.userId = localStorage.getItem('userId');
      this.transcationlist = data.filter((transaction: { senderAccountNumber:string;receiverAccountNumber:string; }) => transaction.senderAccountNumber ==accountNumber || transaction.receiverAccountNumber== accountNumber );
      
      this.transcationlist.map((transcation: { transactionDate: any }) => {
        const formattedDate = new Date(transcation.transactionDate);
        transcation.transactionDate = formattedDate.toLocaleString('en-US', { year: 'numeric', month: 'short', day: 'numeric', hour: 'numeric', minute: 'numeric', hour12: true })

      })
      
      this.collectionSize=this.transcationlist.length;
      this.refreshCountries()
      //console.log('utransactions..............', this.transactions, data);
    })
  }
  pay(accountnumber:number,balance:number,type:string){
  
    this.loanservice.updateBalance(accountnumber,balance).subscribe(res=>{console.log(res,"............loan...........");
   
      this.accountService.updateBalance(this.accountDetails.accountNumber,balance).subscribe(res=>{console.log(res,".........");
      this.toastr.success(type+' Paid Successfully!','Success', { positionClass: 'toast-bottom-right' });
      setTimeout(() => {window.location.reload();},2000);
    });
    // this.addTransferRequest=new FundTransfer();
    //   this.addTransferRequest.amount=balance;
    //   this.addTransferRequest.senderAccountNumber=this.accountDetails.accountNumber;
    //   this.addTransferRequest.receiverAccountNumber=accountnumber;
    //   this.addTransferRequest.userId=this.userId;
    //   this.addTransferRequest.name=type;
    //   this.addTransferRequest.ifsccode="";
    //   console.log("loan......",this.addTransferRequest,this.accountDetails)
    //   this.transactionService.addTransfer(this.addTransferRequest).subscribe(res=>{console.log(res,".....success",this.addTransferRequest)});
  });
  }
}

