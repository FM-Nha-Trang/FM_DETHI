import { Injectable } from '@angular/core';
import { HttpClient, HttpErrorResponse, HttpParams, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs/Observable';
import { IUsers } from './interface/users';
import { ITest } from './interface/test';
import { Users } from './models/users';
import { Test } from './models/test';
import { Question } from './models/question';
import { HistoryAnswer } from './models/history_answer';
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

  //GET list of test create by user
  getTestCreate(id: number): Observable<ITest[]> {
  	return this.http
  			   .get<ITest[]>(this.base_url + "api/Tests/List/User/" + id);
  }

  //GET list user has done
  getTestDone(id: number): Observable<ITest[]> {
  	return this.http
  			   .get<ITest[]>(this.base_url + "api/History_answer/List/User/" + id);
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

  //GET Check exist test code 
  checkTestCode(code: string){
  	return this.http
  			   .get<any>(this.base_url + 'api/Tests/Check/'+code);
  }

  //GET list of question method
  getQuestionsList(testCode: string) {
  	return this.http
  			   .get<any>(this.base_url + "api/Questions/List/"+ testCode);
  }

  //POST question method
  postQuestion(question: Question){
  	return this.http
			   .post<any>(this.base_url + "api/Questions/Add",question);
  }

  //POST Answer record
  createAnswerRecord(saveAnswer: HistoryAnswer){
  	return this.http
			   .post<any>(this.base_url + "api/History_Answer/Add",saveAnswer);
  }

  //POST Answer record
  updateAnswerRecord(testCode: string,saveAnswer: HistoryAnswer){
  	return this.http
			   .post<any>(this.base_url + "api/History_Answer/Update/" + testCode,saveAnswer);
  }

  //GET list of test by test code
  getListByTestCode(testCode: string){
  	return this.http
  			   .get<any>(this.base_url + "api/Tests/Get/" + testCode);
  }

  //GET total of user point
  getTotalPoint(userID: string){
  	return this.http
  			   .get<any>(this.base_url + "api/History_Answer/Point/" + userID);
  }

  //Error handler while using method
  errorHandler(error: HttpErrorResponse){
  	return Observable.throw(error.message || "Server error!!!");
  }
}
