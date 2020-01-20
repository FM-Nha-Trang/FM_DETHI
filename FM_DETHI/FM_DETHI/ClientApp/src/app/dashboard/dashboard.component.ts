import { Component, OnInit } from '@angular/core'; 
import { Router } from '@angular/router';
import { UsersService } from '../users.service';
import { TestService } from '../test.service';
import { HistoryAnswer } from '../models/history_answer';
import { timer } from 'rxjs';


@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.scss']
})
export class DashboardComponent implements OnInit {

  private thisUser: object;
  public testList: any[];
  private testCode: string;
  private testTitle: string;
  private testQuestion: any;
  private countQuestion: number;
  private correctAnswer: number;
  private showResult: boolean = false;
  private finalPoint: number;
  private search: boolean = false;
  private searchResult: any;

  constructor(private router: Router, private _testService: TestService) { }

  ngOnInit() {
  	let loggin = sessionStorage.getItem("loggin");
  	if(!loggin){
  		this.router.navigate(['/login']);
  		sessionStorage.clear();
  	}
    this.thisUser = JSON.parse(sessionStorage.getItem("user"));
    this._testService.getTestList(JSON.parse(sessionStorage.getItem("user")).Id)
                     .subscribe((data:any[]) => this.testList = data, error => console.log("failed"));
  }

  //Search test by test code
  searchTestCode(searchString: string){
    this._testService.getListByTestCode(searchString).subscribe(
                                                                data => {
                                                                  this.searchResult = data.Result;
                                                                  this.search = true;
                                                                },
                                                                error => {
                                                                  console.log(error);
                                                                  this.search = false;
                                                                }
                                                               );    
  }

  //Click start test button
  startTest(test_code: string, test_title: string){
    this.testCode = test_code;
    this.testTitle = test_title;
    this._testService.getQuestionsList(this.testCode).subscribe(
      data => {
        this.testQuestion = data;
        console.log(this.testQuestion);
      }
    );
    this.countQuestion = 0;
    this.correctAnswer = 0;
    this.showResult = false;
  }

  //Click re do test button 
  reTest(test_code: string, test_title: string){
    this.testCode = test_code;
    this.testTitle = test_title;
  }

  //Compare answer
  userAnswer(lengthQuestion: number, correctAns: string, event){
    timer(300).subscribe(x => { 
      if(correctAns == event.target.value){
        this.correctAnswer++;
      }
      this.countQuestion++;
      console.log(this.countQuestion);
      if((lengthQuestion-1) < this.countQuestion){
        this.showResult = true;
        this.finalPoint = (this.correctAnswer/lengthQuestion)*100;
        let curentDate = new Date();
        let date_str = (curentDate.getFullYear() + '-' + this.convertTime((curentDate.getMonth() + 1).toString()) + '-' + this.convertTime(curentDate.getDate().toString()) + 'T' + this.convertTime(curentDate.getHours().toString()) + ":" + this.convertTime(curentDate.getMinutes().toString()) + ":" + this.convertTime(curentDate.getSeconds().toString())).toString();
        let record = new HistoryAnswer(JSON.parse(sessionStorage.getItem("user")).Id,this.testCode,date_str,this.finalPoint);
        this._testService.createAnswerRecord(record).subscribe(
          data => console.log("success"),
          error => console.log("failed")
        ); 
      }
    });
  }

  convertDate(dateTime: string): string{
    let currentDate: string = dateTime.split("T")[0];
    let getDate: string[] = currentDate.split('-');
    let format_day = getDate[2] + '/' + getDate[1] + '/' + getDate[0];
    return format_day;
  }

  convertTime(numberInsert: string): string {
    if(numberInsert.length < 2){
      numberInsert = '0'+numberInsert;
    }
    return numberInsert;
  }

  reloadData(){
    location.reload();
  }

}
