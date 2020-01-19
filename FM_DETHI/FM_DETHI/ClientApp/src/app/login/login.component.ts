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

  public errorSubmit: boolean = false ;

  public errorMessage: string;

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
          if(data.Data[0]){
            sessionStorage.setItem("user", JSON.stringify(data.Data[0]));
            console.log('log success');
            this.router.navigate(['dashboard']);
          } else {
            this.router.navigate(['login']);
          }
  			}, 
  			error => {this.errorSubmit = true; this.errorMessage= error.error.Message}
  		);
  }

}
