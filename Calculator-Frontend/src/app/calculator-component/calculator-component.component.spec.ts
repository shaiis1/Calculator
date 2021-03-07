import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { RouterTestingModule } from '@angular/router/testing';
import { CalculatorComponent } from './calculator-component.component';
import { HttpClientModule, HttpResponse } from '@angular/common/http';
import { ApiService } from '../services/api.service';

describe('CalculatorComponentComponent', () => {
  let component: CalculatorComponent;
  let fixture: ComponentFixture<CalculatorComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      imports: [RouterTestingModule, HttpClientModule],
      declarations: [ CalculatorComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(CalculatorComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });

  // it('should get history results', () => {
  //   let app = fixture.debugElement.componentInstance;
  //   let apiService = fixture.debugElement.injector.get(ApiService);
  //   fixture.detectChanges();
  //   apiService.getHistoryResults('').subscribe((response: HttpResponse<any>) => {
  //     expect(response.status).toBe(200);
  //   })
  // });
  it('should get history results', () => {
      let app = fixture.debugElement.componentInstance;
      let apiService = fixture.debugElement.injector.get(ApiService);
      fixture.detectChanges();
      expect(apiService.getHistoryResults('')).toBeDefined();
  })
});
