import { Injectable } from "@angular/core";
import { Resolve, ActivatedRouteSnapshot, RouterStateSnapshot } from "@angular/router";
import { Observable } from "rxjs";
import { ApiService } from "../services/api.service";

@Injectable({
    providedIn: 'root'
})

export class CalculatorResolver implements Resolve<Observable<any>>{
    
    constructor(private apiService: ApiService){}

    resolve(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): Observable<any>{
        return this.apiService.getHistoryResults('');
    }
}