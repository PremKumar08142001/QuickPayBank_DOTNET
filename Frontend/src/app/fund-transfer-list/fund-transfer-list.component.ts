import { Component, OnInit } from '@angular/core';
import { FundTransfer } from '../models/FundTransfer';
import { FundTransferService } from '../services/fund-transfer.service';

@Component({
  selector: 'app-fund-transfer-list',
  templateUrl: './fund-transfer-list.component.html',
  styleUrls: ['./fund-transfer-list.component.css']
})
export class FundTransferListComponent implements OnInit {

  transfers: FundTransfer[] = [];
  

  constructor( private transferservice: FundTransferService) { }

  ngOnInit(): void {
    this.transferservice.getTransaction().subscribe({
      next : (transfers) => {
        console.log(transfers);
        this.transfers = transfers;
      },
      error: (response) => {
        console.log(response);
      }
    });
  }

}
