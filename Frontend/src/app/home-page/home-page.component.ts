import { animate, style, transition, trigger } from '@angular/animations';
import { HttpClient } from '@angular/common/http';
import { Component, ElementRef, ViewChild } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';

@Component({
  selector: 'app-home-page',
  templateUrl: './home-page.component.html',
  styleUrls: ['./home-page.component.css'],
  animations:[
    trigger('fade', [
     transition(':enter', [
       style({ opacity: 0 }),
       animate('500ms', style({ opacity: 1 }))
     ]),
     transition(':leave', [
       animate('500ms', style({ opacity: 0 }))
     ])
   ])
 ]
})
export class HomePageComponent {
  constructor(private http: HttpClient, private activeRoute: ActivatedRoute,private route:Router ) { }
  @ViewChild('allSections') allSections!: ElementRef<HTMLElement>;

  public navigateToSection(section: string) {
    window.location.hash = '';
    window.location.hash = section;
}
  public login(){
    this.route.navigate(["login"]);
  }

}
