import { NgModule, Component } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { CommonModule } from '@angular/common';
import { CalculatorComponent } from './calculator-component/calculator-component.component';
import { HomePageComponent } from './home-page/home-page.component';
import { CalculatorResolver } from './resolvers/calculator.resolver';

const routes: Routes = [
  { path: '', redirectTo: '/home', pathMatch: 'full'},
  { path: 'home', component: HomePageComponent },
  { path: 'calculator', component: CalculatorComponent, resolve: {historyResults: CalculatorResolver}}
];

@NgModule({
  declarations: [],
  imports: [
    CommonModule,
    RouterModule.forRoot(routes)
  ],
  exports: [RouterModule]
})
export class AppRoutingModule { }
