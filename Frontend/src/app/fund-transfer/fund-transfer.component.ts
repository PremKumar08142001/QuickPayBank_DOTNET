import { Component, OnInit } from '@angular/core';
import { FundTransferService } from '../services/fund-transfer.service';
import { FormBuilder, FormControl, Validators } from '@angular/forms';
import { FundTransfer } from '../models/FundTransfer';
import { AccountService } from '../services/account.service';
import { Router } from '@angular/router';
@Component({
  selector: 'app-fund-transfer',
  templateUrl: './fund-transfer.component.html',
  styleUrls: ['./fund-transfer.component.css']
})
export class FundTransferComponent implements OnInit {
  userId:any;
  userName:any;
  addTransferRequest: FundTransfer = new FundTransfer()
  //senderAccountNumber:any;
  userdetails:any
    ifsc_regex = "^QPAY0[A-Z0-9]{6}$";
  transfer_form = this.formbuilder.group({
    receiverAccountNumber: new FormControl('',[Validators.required,Validators.minLength(10)]),
    ifscCode: new FormControl('',[Validators.required,Validators.minLength(10),Validators.pattern(this.ifsc_regex)]),
    receiverAccountName: new FormControl ('',[Validators.required,Validators.minLength(3)]),
    amount: new FormControl('',[Validators.required]),
   
    });
 
  constructor(private transferService: FundTransferService, private formbuilder : FormBuilder,private accountservice:AccountService,private router:Router){};
  get receiverAccountNumber() {
    return this.transfer_form.get('receiverAccountNumber');
    }
  get ifscCode() {
    return this.transfer_form.get('ifscCode');
    }
  get receiverAccountName() {
    return this.transfer_form.get('receiverAccountName');
    }
  get amount() {
    return this.transfer_form.get('amount');
    }  
    
  ngOnInit(): void{
this.userId=localStorage.getItem("userId")
this.userName=localStorage.getItem("name")
  }
  addTransfer()
  {
    this.addTransferRequest.userId=this.userId;
    
    this.transferService.addTransfer(this.addTransferRequest)
    .subscribe(res=>{
      
      console.log(res,"success..............")

        const overlay = document.querySelector('.overlay') as HTMLElement;
        overlay.style.display = 'flex';
        this.accountservice.accountByName(this.userName).subscribe(res=>{this.userdetails=res;
          this.accountservice.updateBalance(this.userdetails.accountNumber,this.addTransferRequest.amount).subscribe(res=>console.log(res,"........."));
        
        });
        this.accountservice.updateBalance(this.addTransferRequest.receiverAccountNumber,-this.addTransferRequest.amount).subscribe(res=>console.log(res,"........."));

        const completion = overlay.querySelector('.completion') as HTMLElement;
        completion.style.backgroundColor = 'green'
        completion.style.color='white'
        completion.innerHTML="Successful"
        setTimeout(() => {
          // Pause the animation by removing the opacity transition
          completion.style.opacity = '0';
          setTimeout(() => {
            overlay.style.display = 'none';
          }, 1000);
        }, 1000);
       // Adjust the value to adjust the duration of loading
      
       setTimeout(() => {this.router.navigate(["dashboard"])},1000);
    },
      err => {
        console.log(err,"........");
      
          const overlay = document.querySelector('.overlay') as HTMLElement;
          overlay.style.display = 'flex';
        
          const completion = overlay.querySelector('.completion') as HTMLElement;
          completion.style.backgroundColor = 'rgb(233, 58, 45)'; // Set the desired style for rejection
          completion.style.color='white'
          completion.innerHTML="Rejected"
        
          setTimeout(() => {
            // Pause the animation by removing the opacity transition
            completion.style.opacity = '0';
            setTimeout(() => {
              overlay.style.display = 'none';
            }, 2000);
          }, 1000);
        
      }
    );
  }
  
}
