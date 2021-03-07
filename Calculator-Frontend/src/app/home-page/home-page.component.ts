import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-home-page',
  templateUrl: './home-page.component.html',
  styleUrls: ['./home-page.component.css']
})
export class HomePageComponent implements OnInit {
  
  
   name = 'Shai Israeli';
   email = 'shaiisraeli7@gmail.com';

  constructor(private router: Router){}
  ngOnInit(): void {
  }

  btnClick(){
    this.router.navigateByUrl('/calculator');
  }
}
