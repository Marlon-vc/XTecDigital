import { ComponentFixture, TestBed } from '@angular/core/testing';

import { InitializeExcelComponent } from './initialize-excel.component';

describe('InitializeExcelComponent', () => {
  let component: InitializeExcelComponent;
  let fixture: ComponentFixture<InitializeExcelComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ InitializeExcelComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(InitializeExcelComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
