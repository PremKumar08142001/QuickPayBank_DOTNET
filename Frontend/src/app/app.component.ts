import { HttpClient } from '@angular/common/http';
import { Component, ElementRef, OnInit, Renderer2, ViewChild } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {
  constructor(private http: HttpClient, private activeRoute: ActivatedRoute,private route:Router,private renderer: Renderer2,private router:Router ) { }
  title = 'QuickPAy';
  isToggled: boolean = false;

ngOnInit(): void {
  // if(localStorage?.getItem('bearerToken')==null){
  //   console.log(localStorage?.getItem('bearerToken'));
  //   this.content1.nativeElement.style.top="0";
  // }
  // else{
  //   this.content1.nativeElement.style.top="6vh";
  // }
  
}
flager(){
  if(localStorage?.getItem('bearerToken')==null){
    return false;
  }
  return true
}
// styler(){
//   this.renderer.setProperty(this.myCheckbox.nativeElement, 'checked', false);

// }
logout(){
  if(localStorage?.getItem('bearerToken')!=null){
    localStorage.clear();
    
  this.route.navigate(["home"]);
  } 
}
toggleSidebar() {
  const wrapper = document.getElementById('wrapper');
  if (wrapper) {
  wrapper.classList.toggle('toggled');
  this.isToggled = !this.isToggled;
}
}
public getRole(){
  return localStorage.getItem("role")
}
public profile(){
  this.router.navigate(['/profile', localStorage.getItem("name")]);
}
}