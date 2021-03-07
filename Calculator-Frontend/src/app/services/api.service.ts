import { Injectable } from "@angular/core";
import { HttpHeaders, HttpParams, HttpClient, HttpErrorResponse } from "@angular/common/http";
import { Router } from "@angular/router";
import { HttpMethodType } from "../utils/general.enums";
import { Observable, throwError } from "rxjs";
import { catchError, map ,share} from 'rxjs/operators';
import { environment } from "../../environments/environment";

@Injectable({
    providedIn: 'root'
})

export class ApiService{
    baseApiUrl: string;
    
    httpOptions={
        headers: new HttpHeaders({
            'Content-Type': 'application/json'
        }),
        params: new HttpParams({})
    }

    constructor(private http: HttpClient, private router: Router){
        this.baseApiUrl = environment.apiUrl;
    }

    public calculate(data: any){
        debugger
        var apiUrl = this.baseApiUrl + 'Convertor/ConvertEquation';
        return this.sendApiRequest(HttpMethodType.POST, data, this.httpOptions, apiUrl)
    }

    public getHistoryResults(data: any){
        var apiUrl = this.baseApiUrl + 'convertor/GetHistoryResult';
        return this.sendApiRequest(HttpMethodType.GET, data, this.httpOptions, apiUrl);
    }

    sendApiRequest(method: HttpMethodType, data: any, options: any, serverUrl: string): Observable<any> 
    {
        if(method == HttpMethodType.GET){
            return this.http.get(serverUrl, {params: data}).pipe(
                map(data => {
                    return data;
                }), (catchError(this.handleError))
            )
        }
        else
      {
          return this.http.post(serverUrl,data,options).pipe(
            map((data:any)=> {
              return data;
            }), (catchError(this.handleError))
          )

      }
    }

    handleError(error: HttpErrorResponse){
        if(error instanceof HttpErrorResponse){
            if(error.error instanceof ErrorEvent) {
                console.log("Error Event");
            }
            else{
                console.log(`error status : ${error.status} ${error.statusText}`);
                this.router.navigateByUrl("/home");
            }
        }
        else{
            console.error("some thing else happened");
        }
        return throwError(error);
    }
}