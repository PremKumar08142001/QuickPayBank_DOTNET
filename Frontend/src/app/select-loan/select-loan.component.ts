import { Component, OnInit } from '@angular/core';
import { Observable } from 'rxjs';
import { loanOffered } from 'src/app/models/loanOffered';
import { LoanOfferedService } from 'src/app/services/loan-offered.service';
import { LoanAppliedService } from '../services/loan-applied.service';
import { loanApplied } from '../models/loanApplied';
import { AccountService } from '../services/account.service';
import { FundTransfer } from '../models/FundTransfer';
import { FundTransferService } from '../services/fund-transfer.service';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-select-loan',
  templateUrl: './select-loan.component.html',
  styleUrls: ['./select-loan.component.css']
})
export class SelectLoanComponent implements OnInit {
  loanForm:FormGroup;
loan:any;
  selectedLoanType : any;
  selectedLoanInterest : any;
  desiredAmount : number=10000;
  desiredTenure: number=1;
  allLoansOffered!:Observable<loanOffered[]>;
  loaninterest=0;
  userName:any;
  userId:any;
  userExist:any;
  accountDetails:any;
  addTransferRequest: any = new FundTransfer()
constructor(private loanOfferedService : LoanOfferedService,private loanapplied1:LoanAppliedService,private accountservice:AccountService,private transferService:FundTransferService,private toastr: ToastrService){
  this.loanForm = new FormGroup({
    desiredAmount: new FormControl ('',[Validators.required,Validators.min(1000)]),
    desiredTenure: new FormControl('',[Validators.required])

    
  });
}

 ngOnInit(): void {
    this.getAllLoansOffered();
    this.userName=localStorage.getItem('name');
    this.userId=localStorage.getItem("userId")
  }
  getDesiredAmount(){
    return this.loanForm.get('desiredAmount');
  }

  getDesiredAmountErrorMessage(){
    if(this.getDesiredAmount()?.invalid && (this.getDesiredAmount()?.dirty || this.getDesiredAmount()?.touched))
    if(this.getDesiredAmount()?.hasError("required"))
        return "Desired Amount is required"
    else if(this.getDesiredAmount()?.hasError("min"))
        return " Amount should be greater than 1000"

    return "";
    
  }

  getAllLoansOffered(){
        this.allLoansOffered = this.loanOfferedService.getloansOffered();
        this.allLoansOffered.forEach(a=>console.log(a));

  }
  // selectLoan(){
  //   this.selectedLoanType =  document.getElementById('laonType').innerHTML;
  //   this.selectedLoanInterest = document.getElementById('interest').innerHTML;
  //  // console.log(this.selectedLoanInterest);
  // }


      public onOpenApplyLoanModal(loanOfferedId : any,loantype:any): void {
        this.loaninterest=loanOfferedId;
        this.selectedLoanType=loantype;
    const container = document.getElementById('select-loan-container');
    const button = document.createElement('button');
    button.type = 'button';
    button.style.display = 'none';
    button.setAttribute('data-bs-toggle', 'modal');
    button.setAttribute('data-bs-target', '#applyLoanModal');
    container?.appendChild(button);
    button.click();
    console.log("it is clicking "+loanOfferedId);
  }
amountoPay(){
 // return this.desiredAmount*this.loaninterest/1200
  return Math.ceil(((this.desiredTenure*this.desiredAmount*this.loaninterest)/100)+this.desiredAmount)
}
  applyLoan( amount:number, months:number){
    this.addTransferRequest = new FundTransfer()
this.loan=new loanApplied();
this.loan.loanType=this.selectedLoanType;
this.loan.userId=this.userId
this.loan.userName=this.userName;
this.loan.loanAmount=amount
this.loan.tenure=months
this.loan.interest=this.loaninterest
this.loan.amountToPay=Math.ceil(((months*amount*this.loaninterest)/100))+amount
//this.accountService.addAccount(this.accou).subscribe(res=>console.log("jkjkkk",res));
this.loanapplied1.addAppliedLoans(this.loan).subscribe((loanadded:any)=>{console.log("jkjkkk....loan",loanadded)


this.accountservice.accountByName(this.userName).subscribe(res=>{this.accountDetails=res;

  this.accountservice.updateBalance(this.accountDetails.accountNumber,-amount).subscribe(res=>{console.log(res,".........");
  this.addTransferRequest.amount=amount;
  this.addTransferRequest.senderAccountNumber=String(loanadded.loanAppliedID);
  this.addTransferRequest.receiverAccountNumber=String(this.accountDetails.accountNumber);
  this.addTransferRequest.userId=this.userId;
  this.addTransferRequest.name=this.selectedLoanType;
  this.addTransferRequest.ifsccode=this.accountDetails.branchCode;
  console.log("loan......",this.addTransferRequest,this.accountDetails)
  this.transferService.addTransfer(this.addTransferRequest).subscribe(res=>{console.log(res,".....loan",this.addTransferRequest)});
});
  

});
this.toastr.success(this.selectedLoanType+' Applied Successfully!','Success', { positionClass: 'toast-bottom-right' });
   
});

    
console.log(this.loan);
this.desiredAmount = 10000;
this.desiredTenure = 1;  
  }
}



