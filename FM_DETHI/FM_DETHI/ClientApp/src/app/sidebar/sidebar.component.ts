import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-sidebar',
  templateUrl: './sidebar.component.html',
  styleUrls: ['./sidebar.component.scss']
})
export class SidebarComponent implements OnInit {

  private thisUser: object;

  constructor(private router: Router) { }

  ngOnInit() {
    this.thisUser = JSON.parse(sessionStorage.getItem("user"));
  }


  toggleSidebar(){
   document.getElementById("sidebar").classList.toggle('active');
  }

  userLogout(){
	sessionStorage.clear();
	this.router.navigate(['/login']);
  }

}
