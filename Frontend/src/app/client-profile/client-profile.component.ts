import { Component } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { FundTransferService } from '../services/fund-transfer.service';
import { AccountService } from '../services/account.service';
import { AuthenticationService } from '../services/authentication.service';
@Component({
  selector: 'app-client-profile',
  templateUrl: './client-profile.component.html',
  styleUrls: ['./client-profile.component.css']
})
export class ClientProfileComponent {
user:any;
name:any;
  username:any;
  email: string = 'johndoe@example.com';
  phone: string = '123-456-7890';
  gender: string = 'Male';
  address: string = '123 Main St, City, Country';

  branchId: string = '123'
  accountNumber: string = '1234567890'

  activityData: any;
  transactions: any;
  userId:any;
  userDetails:any;
  constructor(private accountService: AccountService,private authService:AuthenticationService, private transcationService:FundTransferService, private route: ActivatedRoute) {}

  ngOnInit() {
    
    this.username = this.route.snapshot.paramMap.get('userName');
    this.authService.getUserDetails(this.username).subscribe(resp=>{
      
      console.log(this.username,"jk..........................")
      this.userDetails=resp;
      console.log(this.userDetails,"/////////////////////////////////////////////////....",)
      this.accountService.getAccountByUserId(this.userDetails.userId).subscribe(data => {
        this.branchId = data.branchCode
        this.accountNumber = data.accountNumber
        this.username = data.userName
        console.log('userProfile ', data);
        this.authService.getUserDetails(this.username).subscribe(data => {
          console.log(data,"/////////////////////////////////////////////////",this.username)
       
          // this.username = data.userName;
          // this.email = data.email;
          // this.phone = data.phoneNumber;
          this.gender = data.userGender;
          // this.address = data.userAddress;
          this.user=data;
          console.log(this.user,"//////////////");
          this.transcationService.getTransaction().subscribe(data => {
            this.transactions = data.filter((transaction: { userId: string; }) => 
            transaction.userId == this.userDetails.userId)
            console.log('accountActivity ', this.transactions);
            this.loadActivityData(this.transactions);
          })
      
        })
      })
    });

  }
  getAvatarImage(gender: string): string {
    if (gender === 'Female') {
      return '/assets/img/woman.png';
    } else {
      return '/assets/img/man (1).png';
    }
  }
  loadActivityData(transactions: any) {

    // this.activityData = [
    //   {
    //     name: 'Account Activity',
    //     series: [
    //       { name: 'Jan', value: 20 },
    //       { name: 'Feb', value: 30 },
    //       { name: 'Mar', value: 50 },
    //       { name: 'Apr', value: 10 },
    //       { name: 'May', value: 20 },
    //       { name: 'Jun', value: 10 },
    //     ]
    //   }
    // ];

    const groupedData = transactions.reduce((result: any, transaction: any) => {
      const month = new Date(transaction.transactionDate).toLocaleString('en-US', { month: 'short' });
      const existingMonth = result.find((entry: { name: string; }) => entry.name === month);
      
      if (existingMonth) {
        existingMonth.value++;
      } else {
        result.push({ name: month, value: 1 });
      }
      
      return result;
    }, []);
  
    this.activityData = [{
      name: 'Account Activity',
      series: groupedData
    }];

    console.log('transformedAccountActivity ', this.activityData);


  }

  
  

}
