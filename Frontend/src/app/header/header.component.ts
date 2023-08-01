import { Component } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.css']
})
export class HeaderComponent {
  constructor(private activeRoute: ActivatedRoute,private route:Router ) { }
  public loginSignup(){
    //this.bankService.redirectToLoginSignup();
  }
  public navigateToSection(section: string) {
    window.location.hash = '';
    window.location.hash = section;
}
public login(){
  this.route.navigate(["login"]);
}
}