import { Component, Input, OnInit } from '@angular/core';
import { NgbActiveModal, NgbModal } from '@ng-bootstrap/ng-bootstrap';

import {MatDialog, MAT_DIALOG_DATA, MatDialogRef, MatDialogModule} from '@angular/material/dialog';

import { ModalComponent } from '../modal/modal.component';
import { ModalService } from 'src/app/services/modal.service';
import { LoanAppliedService } from 'src/app/services/loan-applied.service';
import { loanApplied } from 'src/app/models/loanApplied';
import { Observable } from 'rxjs';
import { LoanOfferedService } from '../services/loan-offered.service';
 declare var $ : any;
@Component({
  selector: 'app-approve-loan',
  templateUrl: './approve-loan.component.html',
  styleUrls: ['./approve-loan.component.css']
})

export class ApproveLoanComponent implements OnInit {

  msg : any  = '';
  radValue : any  = '';
  loans! :Observable<loanApplied[]>

  constructor(private loanAppliedService : LoanAppliedService,private loanoffered : LoanOfferedService, public dialogRef : MatDialog, public serviceModal : ModalService) {
	 
  }
  getLoansApplied(){
    this.loans = this.loanAppliedService.getAllAppliedLoans();
  }

  openDialog(){
    this.dialogRef.open(ModalComponent,{
      data :{
        name : "Labaik"
      }
      });
  }
    public onOpenRejectModal(): void {
    const container = document.getElementById('main-container');
    const button = document.createElement('button');
    button.type = 'button';
    button.style.display = 'none';
    button.setAttribute('data-toggle', 'modal');
    button.setAttribute('data-target', '#rejectLoanModal');
    container?.appendChild(button);
    button.click();
  }
      public onOpenApproveModal(): void {
    const container = document.getElementById('main-container');
    const button = document.createElement('button');
    button.type = 'button';
    button.style.display = 'none';
    button.setAttribute('data-toggle', 'modal');
    button.setAttribute('data-target', '#approveLoanModal');
    container?.appendChild(button);
    button.click();
  }

  onCloseRejectModal(){
    this.msg="";
  }
  rejectReq(){
  //   this.msg = document.getElementById('comment');
    console.log(this.msg);
    this.msg="";
  }
 
    // Fires on Radio button's change event
    onRadioChange(event:any){
    // Get the selected value
    this.radValue = event.target.value; 
  }
   approveReq(){
      // console.log(this.radValue);
      //  this.radValue = '';
      
  }

  ngOnInit(): void {
    this.getLoansApplied();
  }
  
}
