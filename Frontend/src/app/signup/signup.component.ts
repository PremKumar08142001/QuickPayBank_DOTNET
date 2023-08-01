import { Component,ElementRef, VERSION, ViewChild } from '@angular/core';
import { FormBuilder, FormControl, Validators } from '@angular/forms';
import { AuthenticationService } from '../services/authentication.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-signup',
  templateUrl: './signup.component.html',
  styleUrls: ['./signup.component.css']
})
export class SignupComponent {
  constructor(private formbuilder:FormBuilder,private auth:AuthenticationService,private route: Router){ 
   
  }
  @ViewChild('container')
  container!: ElementRef;
  matcher="^[a-z0-9._%+-]+@[a-z0-9.-]+\\.[a-z]{2,4}$"
    loginform = this.formbuilder.group({
      email:new FormControl('',[Validators.required,Validators.pattern(this.matcher)]),
      password: new FormControl('',[Validators.required,Validators.minLength(4)]),
      
    });
    get email(){
      return this.loginform.get('email'); 
    }
  
    get password(){
      return this.loginform.get('password');
    }
  
    onSubmit() {
  
      if(this.auth.authenticateUser(this.loginform.value)){
        this.route.navigate(['home'])
      }
      else{
     alert("Email does not exist or wrong password ")
      }
    }
  signIn() {
    this.container.nativeElement.classList.add('right-panel-active');
  }
  
  signUp() {
    this.container.nativeElement.classList.remove('right-panel-active');
  }
  
}
