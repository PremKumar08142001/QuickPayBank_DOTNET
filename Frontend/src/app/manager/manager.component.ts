import { Component, OnInit } from '@angular/core';
import { AccountService } from '../services/account.service';
import { NgbActiveModal, NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { BranchServiceService } from '../services/branch-service.service';
@Component({
  selector: 'app-manager',
  templateUrl: './manager.component.html',
  styleUrls: ['./manager.component.css']
})
export class ManagerComponent implements OnInit{
  branch:any;
  page = 1;
	pageSize = 4;
	accountlist:any;
  accounts:any;
  collectionSize:any;

	constructor(private accountService:AccountService, private branchService:BranchServiceService) {
    
  }
  
  ngOnInit(): void {
    this.branchService.getBranchByName(localStorage.getItem("name")).subscribe(resp=>{this.branch=resp;
      this.accountService.getAccounts(this.branch.branchCode).subscribe(resp=>{this.accountlist=resp;
        this.collectionSize=this.accountlist.length;
        this.refreshCountries()
      });});
      
    
  }

	refreshCountries() {
		this.accounts = this.accountlist.map((accountlist: any, i: number) => ({ index: i + 1, ...accountlist })).slice(
			(this.page - 1) * this.pageSize,
			(this.page - 1) * this.pageSize + this.pageSize,
		);
	}

	open() {
		console.log("nothing")
	}
}
