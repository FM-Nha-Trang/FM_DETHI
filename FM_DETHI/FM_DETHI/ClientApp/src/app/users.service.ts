import { Injectable } from '@angular/core';
import { HttpClient, HttpErrorResponse, HttpParams, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs/Observable';
import { IUsers } from './interface/users';
import { Users } from './models/users';
import { Login } from './models/login';
import 'rxjs/add/operator/catch';
import 'rxjs/add/observable/throw';


@Injectable({
  providedIn: 'root'
})
export class UsersService {

  base_url: string = "https://localhost:5001/api/";

  constructor(private http: HttpClient) { }

  //GET users method
  getUsers(): Observable<IUsers[]> {
  	return this.http
  			   .get<IUsers[]>(this.base_url + "Users/List");
  }


  //POST user method, use to create a user
  createUsers(users: Users){
	return this.http
			   .post<any>( this.base_url + "Users/Register", users );
  }

  //POST user auth
  authUser(login: Login){
  	return this.http
			   .post<any>(this.base_url + "Users/Login",login);
  }

  //Error handler while using method
  errorHandler(error: HttpErrorResponse){
  	return Observable.throw(error.message || "Server error!!!");
  }
}
