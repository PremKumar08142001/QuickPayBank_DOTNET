import { Component, Inject, OnInit } from '@angular/core';
import { MatIconButton } from '@angular/material/button';
import { MAT_DIALOG_DATA, MatDialog } from '@angular/material/dialog';

@Component({
  selector: 'app-modal',
  templateUrl: './modal.component.html',
  styleUrls: ['./modal.component.css']
})
export class ModalComponent implements OnInit {
  value = '';
  firstName:any;
 constructor(private dialog : MatDialog){

 }
     closeDialog(){
    this.dialog.closeAll();
  }

  rejectReq(){
    
  }
  // constructor(@Inject(MAT_DIALOG_DATA) public data){
  //   this.firstName = data.name;
  // }

  ngOnInit(): void {  
  }

}
