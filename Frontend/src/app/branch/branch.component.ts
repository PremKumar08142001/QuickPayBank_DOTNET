import { Component, OnInit } from '@angular/core';
import { Branch } from '../models/branch';
import { BranchServiceService } from '../services/branch-service.service';
import { AuthenticationService } from '../services/authentication.service';
import { Router } from '@angular/router';
import { loanApplied } from '../models/loanApplied';
import { loanOffered } from '../models/loanOffered';
import { LoanOfferedService } from '../services/loan-offered.service';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-branch',
  templateUrl: './branch.component.html',
  styleUrls: ['./branch.component.css']
})
export class BranchComponent implements OnInit{
loan_type:any;
loan_url:any;
loan_interest:any;
  page = 1;
	pageSize = 4;
	branchlist:any;
  branches:any;
  collectionSize:any;

  managerList:any;
  manager:any;
  branchname:any;
  branch:Branch=new Branch();
loan:any

  managername: any
  manageremail: any
  managerphone: any
  managergender: any
  manageraddress: any
  managerpassword: any

  constructor(private branchservice:BranchServiceService,private authService:AuthenticationService,private router: Router,private loanoffered:LoanOfferedService,private toastr:ToastrService){
 
  }

  ngOnInit(): void {
    this.authService.getAllManagers().subscribe(res=>
      {
        console.log('managerlist', res);
        this.managerList=res;console.log(this.managerList);
      })

    // this.branchservice.getAllBranches().subscribe((resp: any)=>{this.branchlist=resp;
    //     console.log('branchlist', resp);
    //     this.collectionSize=this.branchlist.length;
    //     this.refreshCountries()
    //   });


    this.getBranches()
    
    this.branchservice.refreshBranches().subscribe(data => {
      this.getBranches();
    });
      
  }

  getBranches() {
    this.branchservice.getAllBranches().subscribe(data => {
      this.branches = data
      this.collectionSize = this.branches.length;
      this.refreshCountries();
    })
  }
  
  public onOpenRegistrationModal() : void {
    const container = document.getElementById('opeen');
    const button = document.createElement('button');
    button.type = 'button';
    button.style.display = 'none';
    button.setAttribute('data-bs-toggle', 'modal');
    button.setAttribute('data-bs-target', '#addManagerModal');
    container?.appendChild(button);
    button.click();
  }
  
  public onOpenLoanModal() : void {
    const container = document.getElementById('opeen');
    const button = document.createElement('button');
    button.type = 'button';
    button.style.display = 'none';
    button.setAttribute('data-bs-toggle', 'modal');
    button.setAttribute('data-bs-target', '#loan');
    container?.appendChild(button);
    button.click();
  }
  public onOpenModal(): void {
  
    const container = document.getElementById('opeen');
    const button = document.createElement('button');
    button.type = 'button';
    button.style.display = 'none';
    button.setAttribute('data-bs-toggle', 'modal');
    button.setAttribute('data-bs-target', '#updateUserModal');
    container?.appendChild(button);
    button.click();
    
  }
  onAddloan() {
    // this.manager.managername = this.managername
    // this.manager.manageremail = this.manageremail
    // this.manager.managerphone = this.managerphone
    // this.manager.manageraddress = this.manageraddress
    // this.manager.managerpassword = this.managerpassword
this.loan=new loanOffered()
        this.loan.loanType= this.loan_type,
        this.loan.interest=this.loan_interest,
        this.loan.image=this.loan_url,
        console.log( this.loan,"............loan");
    this.loanoffered.addLoan(this.loan).subscribe(data => {
      console.log( data, this.loan,"............loan");
      this.toastr.success(' Loan Added Successfully!','Success', { positionClass: 'toast-bottom-right' });
      setTimeout(() => {window.location.reload();},2000);
    })
    this.loan_type=""
    this.loan_url=""
    this.loan_interest=0

  }

  onAddManager() {
    // this.manager.managername = this.managername
    // this.manager.manageremail = this.manageremail
    // this.manager.managerphone = this.managerphone
    // this.manager.manageraddress = this.manageraddress
    // this.manager.managerpassword = this.managerpassword
    const manager = {
        "userName": this.managername,
        "userEmail": this.manageremail,
        "userPhone": this.managerphone,
        "userGender": this.managergender,
        "userAddress": this.manageraddress,
        "password": this.managerpassword,
        "role": ""
    }
    console.log('manager ', manager);
    this.authService.registerManager(manager).subscribe(data => {
      console.log('managerRegistered ', data, this.manager);
      this.toastr.success(' Manager Added Successfully!','Success', { positionClass: 'toast-bottom-right' });
      setTimeout(() => {window.location.reload();},2000);
    })
    this.managername=""
    this.manageremail=""
    this.managerphone=""
    this.managergender=""
    this.manageraddress=""
    this.managerpassword=""

  }

  public onCreate():void{
    this.branch.branchName=this.branchname;
    this.branch.userName=this.manager;

    this.branchservice.addBranch(this.branch).subscribe(res=> {
     // console.log("addbran............ch", res)
      this.toastr.success(' Branch Added Successfully!','Success', { positionClass: 'toast-bottom-right' });
      setTimeout(() => {window.location.reload();});     
    },err=>{
      this.toastr.success(' Branch Added Successfully!','Success', { positionClass: 'toast-bottom-right' });
      setTimeout(() => {window.location.reload();});     
    })
   this.branchname=""
   this.manager=""

    // this.accountService.addAccount(this.accou).subscribe(res=>console.log("jkjkkk",res));
  }
  refreshCountries() {
		this.branches = this.branchlist.map((accountlist: any, i: number) => ({ index: i + 1, ...accountlist })).slice(
			(this.page - 1) * this.pageSize,
			(this.page - 1) * this.pageSize + this.pageSize,
		);
	}

}
