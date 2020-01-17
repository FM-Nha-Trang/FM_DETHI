import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { UsersService } from '../users.service';

@Component({
  selector: 'app-history',
  templateUrl: './history.component.html',
  styleUrls: ['./history.component.scss']
})
export class HistoryComponent implements OnInit {

   private thisUser: object;

  constructor(private router: Router) { }

  ngOnInit() {
  	let loggin = sessionStorage.getItem("loggin");
  	if(!loggin){
  		this.router.navigate(['/login']);
  		sessionStorage.clear();
  	}
    this.thisUser = JSON.parse(sessionStorage.getItem("user"));
  }

}
