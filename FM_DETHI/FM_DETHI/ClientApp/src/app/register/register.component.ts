import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Users } from '../models/users';
import { UsersService } from '../users.service';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.scss']
})
export class RegisterComponent implements OnInit {

  /*
  ** username,last_name,first_name,age,gender,password
  ** password_test: Helloworld1
   */
  usersModel = new Users(0,'','','',null , 'Nam', '');

  public errorSubmit: boolean = false ;

  public errorMessage: string;

  public successSubmit: boolean = false;

  public passwordRegex: string = "^(?=.*\d)(?=.*[a-zA-Z]).{8,}$";

  public comparePass: boolean = true; 

  constructor(private _usersService: UsersService, private router: Router) { }

  ngOnInit() {
    let loggin = sessionStorage.getItem("loggin");
    if(loggin){
      this.router.navigate(['/dashboard']);
    }
  }

  onSubmit() {
  	this._usersService
  		.createUsers(this.usersModel)
  		.subscribe(
  			data => {this.errorSubmit = false; this.successSubmit = true;}, 
  			error => {this.errorSubmit = true; this.successSubmit = false; console.log(JSON.stringify(error)); this.errorMessage = error.error.Message; } 
  		);
  }

  //Check current password
  onRetypePass(event){
  	let retypePass = event.target.value;
  	if(this.usersModel.pass != retypePass){
  		this.comparePass = false;
  	} else {
  		this.comparePass = true;
  	}
  }

}
