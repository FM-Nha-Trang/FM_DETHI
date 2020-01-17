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

  constructor(private http: HttpClient) { }

  //GET users method
  getUsers(): Observable<IUsers[]> {
  	return this.http
  			   .get<IUsers[]>("https://localhost:5001/api/Users")
  			   .catch(this.errorHandler);
  }

  //POST user method, use to create a user
  createUsers(users: Users){
	return this.http
			   .post<any>( "https://localhost:5001/api/Users", users );
  }

  //POST user auth
  authUser(login: Login){
  	return this.http
			   .post<any>("https://localhost:5001/api/Users/Login",login);
  }

  //Error handler while using method
  errorHandler(error: HttpErrorResponse){
  	return Observable.throw(error.message || "Server error!!!");
  }
}
