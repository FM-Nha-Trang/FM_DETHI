import { Injectable } from '@angular/core';
import { HttpClient, HttpErrorResponse, HttpParams, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs/Observable';
import { IUsers } from './interface/users';
import { ITest } from './interface/test';
import { Users } from './models/users';
import { Test } from './models/test';
import { Question } from './models/question';
import 'rxjs/add/operator/catch';
import 'rxjs/add/observable/throw';

@Injectable({
  providedIn: 'root'
})
export class TestService {


  base_url: string = "https://localhost:5001/";

  constructor(private http: HttpClient) { }

  //GET list of test method
  getTestList(id: number): Observable<ITest[]> {
  	return this.http
  			   .get<ITest[]>(this.base_url + "api/Tests/List/" + id);
  }

  //POST test method
  postTest(test: Test){
  	return this.http
			   .post<any>(this.base_url + "api/Tests/Add",test);
  }

  //DELETE test method
  deleteTest(testCode: string){
  	return this.http
  			   .delete<any>(this.base_url + `api/Tests/Add/${testCode}`);
  }

  //GET list of question method
  getQuestionsList(testCode: string) {
  	let params = new HttpParams();
	params = params.append('test_code', testCode);
  	return this.http
  			   .get<any>(this.base_url + "api/Questions/List", {params: params})
  			   .catch(this.errorHandler);
  }

  //POST question method
  postQuestion(question: Question){
  	return this.http
			   .post<any>(this.base_url + "api/Tests/Add",question);
  }

  //Error handler while using method
  errorHandler(error: HttpErrorResponse){
  	return Observable.throw(error.message || "Server error!!!");
  }
}