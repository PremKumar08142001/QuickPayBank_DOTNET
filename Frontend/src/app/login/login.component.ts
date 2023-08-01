// import { Component,ElementRef, ViewChild } from '@angular/core';
// import { FormBuilder, FormControl, Validators } from '@angular/forms';
// import { AuthenticationService } from '../services/authentication.service';
// import { Router } from '@angular/router';
// import { Register } from '../models/register';
// @Component({
//   selector: 'app-login',
//   templateUrl: './login.component.html',
//   styleUrls: ['./login.component.css']
// })
// export class LoginComponent {
//   role:any;
//   temp:any;
//   userdata!:Register
//   registerMessage:any
//   registerform:any
//   loginMessage:any
//   @ViewChild('container')
//   container!: ElementRef;
//   constructor(private formbuilder:FormBuilder,private auth:AuthenticationService,private route: Router){ 
   
//    this.registerform = this.formbuilder.group({
//     email:new FormControl('',[Validators.required,Validators.email]),
//     name: new FormControl ('',[Validators.required,Validators.minLength(4)]),
//     address: new FormControl('',[Validators.required,Validators.minLength(4)]),
//     mobile:new FormControl ('',[Validators.required,Validators.minLength(10),Validators.pattern("^[0-9]{1,10}$")]),
//     gender: new FormControl ('',[Validators.required,Validators.minLength(4)]),
//     password1: new FormControl('',[Validators.required,Validators.minLength(4)]),
    
//   });
//   }
 
//     loginform = this.formbuilder.group({
//       userName:new FormControl('',[Validators.required,Validators.minLength(4)]),
//       password: new FormControl('',[Validators.required,Validators.minLength(4)]),
      
//     });
//     get userName(){
//       return this.loginform.get('userName'); 
//     }
  
//     get password(){
//       return this.loginform.get('password');
//     }
  
//     login() {

//         this.auth.authenticateUser(this.loginform.value).subscribe(
//           res => {
//             this.temp=res;
//           this.auth.setBearerToken(this.temp.token)
//           this.role=this.auth.setClames()
//           if(this.role==8){
//           this.route.navigate(["dashboard"]);
//           }
//           else{
//             this.route.navigate(["monitoring"]);
//           }

//           }, err => {
//             this.loginMessage = err.message;
//             console.log(this.loginMessage);
//             if (err.status === 500) {
//               this.loginMessage = 'Unauthorized';
//             } else {
//               this.loginMessage = 'invalid data';
//             }
//           }
//         );
       
       
//     }
//   get email(){
//     return this.registerform.get('email'); 
//   }
//   get name() {
//     return this.registerform.get('name');
//   }
//   get address() {
//     return this.registerform.get('address');
//   }
//   get mobile(){
//     return this.registerform.get('mobile');
//   }
//   get gender(){
//     return this.registerform.get('gender');
//   }
//   get password1(){
//     return this.registerform.get('password1');
//   } 

//   register() {
//     this.userdata=new Register();
//     this.userdata.userName =this.registerform.get('name').value;
//     this.userdata.userEmail =this.registerform.get('email').value;
//     this.userdata.userGender =this.registerform.get('gender').value;
//     this.userdata.userAddress =this.registerform.get('address').value;
//     this.userdata.userPhone =this.registerform.get('mobile').value;
//     this.userdata.password =this.registerform.get('password1').value;
//     // this.userservice.senduser(this.userdata)
//    this.auth.registerUser(this.userdata).subscribe(
//     resp => {
//     }, err => {
//       this.registerMessage = err.message;
//       if (err.status === 500) {
//         this.registerMessage = 'User alreday Exist';
//       } else {
//         this.registerMessage = 'Data is not valid';
//       }
//     }
//   );
//   this.container.nativeElement.classList.remove('right-panel-active');
//   }
//   signIn() {
//     this.container.nativeElement.classList.remove('right-panel-active');
//   }

//   signUp() {
//     this.container.nativeElement.classList.add('right-panel-active');
//   }

// }
import { Component,ElementRef, ViewChild } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { AuthenticationService } from '../services/authentication.service';
import { Router } from '@angular/router';
import { Register } from '../models/register';
@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent {
  role:any;
  temp:any;
  userdata!:Register
  registerMessage:any
  // registerform:any
  loginMessage:any

  registerForm : FormGroup;
  @ViewChild('container')
  container!: ElementRef;
  constructor(private formbuilder:FormBuilder,private auth:AuthenticationService,private route: Router){ 
    
    this.registerForm = new FormGroup({
    email:new FormControl('',[Validators.required,Validators.email]),
    name: new FormControl ('',[Validators.required,Validators.minLength(4)]),
    address: new FormControl('',[Validators.required,Validators.minLength(4)]),
    mobile:new FormControl ('',[Validators.required,Validators.pattern("^[0-9]{10}$")]),
    gender: new FormControl ('',[Validators.required]),
    password1: new FormControl('', [Validators.required, Validators.pattern(/^(?=.*[A-Z])(?=.*[0-9])(?=.*[a-zA-Z0-9])(?=.*[!@#$%^&*()_+\-=[\]{}|\\:;"'<>,.?/~`])[a-zA-Z0-9!@#$%^&*()_+\-=[\]{}|\\:;"'<>,.?/~`]+$/)]),
    
    
  });
  }
 
    loginform = this.formbuilder.group({
      userName:new FormControl('',[Validators.required,Validators.minLength(4)]),
      password: new FormControl('', [Validators.required, Validators.pattern(/^(?=.*[A-Z])(?=.*[0-9])(?=.*[a-zA-Z0-9])(?=.*[!@#$%^&*()_+\-=[\]{}|\\:;"'<>,.?/~`])[a-zA-Z0-9!@#$%^&*()_+\-=[\]{}|\\:;"'<>,.?/~`]+$/)]),
      
    });
    getUserName(){
      return this.loginform.get('userName'); 
    }
  
    getPassword(){
      return this.loginform.get('password');
    }

     getUserNameErrorMessage(){
      if(this.getUserName()?.invalid && (this.getUserName()?.dirty || this.getUserName()?.touched))
      if(this.getUserName()?.hasError("required"))
          return "Username is required"
        else if(this.getUserName()?.hasError('minlength'))
          return "Username is not valid" 
        return "";
    }
    getPasswordErrorMessage(){
      if(this.getPassword()?.invalid && (this.getPassword()?.dirty || this.getPassword()?.touched))
      if(this.getPassword()?.hasError("required"))
          return "Password is required"
        else if(this.getPassword()?.hasError('pattern'))
          return "Password is not valid" 
        return "";
    }
  
    login() {

        this.auth.authenticateUser(this.loginform.value).subscribe(
          res => {
            this.temp=res;
          this.auth.setBearerToken(this.temp.token)
          this.role=this.auth.setClames()
          if(this.role==3){
          this.route.navigate(["monitoring"]);
          }
          else if(this.role==2){
            this.route.navigate(["manager"]);
          }
          else{
            this.route.navigate(["dashboard"]);
          }

          }, err => {
            this.loginMessage = err.message;
            console.log(this.loginMessage);
            if (err.status === 500) {
              this.loginMessage = 'Unauthorized';
            } else {
              this.loginMessage = 'invalid data';
            }
          }
        );
       
       
    }
    getEmail(){
      return this.registerForm.get('email');
    }
    getName(){
      return this.registerForm.get('name');
    }

    getAddress(){
      return this.registerForm.get('address');
    }
    getMobile(){
      return this.registerForm.get('mobile');
    }
    getGender(){
       return this.registerForm.get('gender');
    }
    getPassword1(){
      return this.registerForm.get('password1');
    }

    
    getEmailErrorMessage(){
      if(this.getEmail()?.invalid && (this.getEmail()?.dirty || this.getEmail()?.touched))
      if(this.getEmail()?.hasError("required"))
          return "Email is required"
        else if(this.getEmail()?.hasError('email'))
          return "Email is not valid" 
        return "";
    }
    getNameErrorMessage(){
      if(this.getName()?.invalid && (this.getName()?.dirty || this.getName()?.touched))
      if(this.getName()?.hasError("required"))
          return "Name is required"
        else if(this.getName()?.hasError('minlength'))
          return "Name should not be less the 4 char" 
        return "";
    }
    getPassword1ErrorMessage(){
      if(this.getPassword1()?.invalid &&(this.getPassword1()?.dirty || this.getPassword1()?.touched))
        if(this.getPassword1()?.hasError("required"))
          return "Password is required"
        else if(this.getPassword1()?.hasError('pattern'))
          return "Password is not valid"
        return "";
    }
    getAddressErrorMessage(){
      if(this.getAddress()?.invalid && (this.getAddress()?.dirty || this.getAddress()?.touched))
      if(this.getAddress()?.hasError("required"))
          return "Address is required"
        else if(this.getAddress()?.hasError('minlength'))
          return "Address is not valid" 
        return "";
    }
    getGenderErrorMessage(){
      if(this.getGender()?.invalid && (this.getGender()?.dirty || this.getGender()?.touched))
      if(this.getGender()?.hasError("required"))
          return "Gender is required"
        return "";
    }

    getMobileErrorMessage(){
      if(this.getMobile()?.invalid && (this.getMobile()?.dirty || this.getMobile()?.touched))
      if(this.getMobile()?.hasError("required"))
          return "Mobile Number is required"
        else if(this.getMobile()?.hasError('pattern'))
          return "Mobile Number not valid" 
        // else if(this.getMobile()?.hasError('minlength'))
        //   return "Mobile Number must be 10 digits" 
        // else
        //   return "Mobile Number must be 10 digits" 
        return "";
    }


  register(registrationForm : FormGroup) {
    console.log("...",registrationForm.value);
    this.userdata=new Register();
    this.userdata.userName= registrationForm.value.name;
    this.userdata.userEmail =registrationForm.value.email;
    this.userdata.userGender =registrationForm.value.gender;
    this.userdata.userAddress =registrationForm.value.address;
    this.userdata.userPhone =registrationForm.value.mobile.toString();
    this.userdata.password =registrationForm.value.password1;
    // this.userservice.senduser(this.userdata)
    
   this.auth.registerUser(this.userdata).subscribe(
    resp => {
      console.log("......"+resp+"jkkkkkkkkk")
      this.registerForm.reset()
      this.container.nativeElement.classList.remove('right-panel-active');
    }, err => {
      this.registerMessage = err.message;
      if (err.status === 500) {
        this.registerMessage = 'User alreday Exist';
      } else {
        this.registerMessage = 'Data is not valid';
      }
    }
  );
 
  }
  signIn() {
    this.container.nativeElement.classList.remove('right-panel-active');
  }

  signUp() {
    this.container.nativeElement.classList.add('right-panel-active');
  }

}
  