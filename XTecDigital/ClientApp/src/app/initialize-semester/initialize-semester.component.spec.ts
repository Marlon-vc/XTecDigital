import { ComponentFixture, TestBed } from '@angular/core/testing';

import { InitializeSemesterComponent } from './initialize-semester.component';

describe('InitializeSemesterComponent', () => {
  let component: InitializeSemesterComponent;
  let fixture: ComponentFixture<InitializeSemesterComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ InitializeSemesterComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(InitializeSemesterComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
