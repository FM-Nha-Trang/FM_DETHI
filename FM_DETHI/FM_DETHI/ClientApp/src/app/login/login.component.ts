import { Component, OnInit } from '@angular/core';  
import { Router } from '@angular/router';
import { Login } from '../models/login';
import { UsersService } from '../users.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent implements OnInit {

  constructor(private _usersService: UsersService, private router: Router) { }


  loginModel = new Login('','');

  ngOnInit() {
  	let loggin = sessionStorage.getItem("loggin");
  	if(loggin){
  		this.router.navigate(['/dashboard']);
  	}
  }

  onSubmit() {
  	this._usersService
  		.authUser(this.loginModel)
  		.subscribe(
  			data => {
  				sessionStorage.setItem("loggin", "true");
  				sessionStorage.setItem("user", JSON.stringify(data.Data[0]));
  				this.router.navigate(['/dashboard']);
  			}, 
  			error => console.log(JSON.stringify(error)) 
  		);
  }

}
