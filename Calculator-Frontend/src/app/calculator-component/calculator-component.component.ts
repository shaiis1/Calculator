import { Component, OnInit } from '@angular/core';
import { ApiService } from '../services/api.service';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-calculator-component',
  templateUrl: './calculator-component.component.html',
  styleUrls: ['./calculator-component.component.css']
})
export class CalculatorComponent implements OnInit {
  allResults = [];
  lastResult = '';
  text = '';
  historyResultsText = 'No Rsults Found';

  constructor(private apiService: ApiService, private route: ActivatedRoute) { }

  ngOnInit(): void {
    debugger
    this.route.data.subscribe(
      (data) => {
        this.allResults = data.historyResults.allResults;
        if(this.allResults.length > 0){
        this.historyResultsText = 'History Results:';
        }
      }
    )
  }

  pressKey(key: string) {
    this.text += key;
  }

  allClear() {
    this.text = '';
  }

  getAnswer(){
    debugger
    var calcRequest = {
      CalcString: this.text
    }
    this.apiService.calculate(calcRequest).subscribe(
      (data) => {
        this.lastResult = data.result;
        this.allResults = data.allResults;
        if(this.allResults.length > 0){
        this.historyResultsText = 'History Results:';
        }
        this.text = '';
      }
    );
  }

}
