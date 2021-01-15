import { ComponentFixture, TestBed } from '@angular/core/testing';

import { EvaluateTaskComponent } from './evaluate-task.component';

describe('EvaluateTaskComponent', () => {
  let component: EvaluateTaskComponent;
  let fixture: ComponentFixture<EvaluateTaskComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ EvaluateTaskComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(EvaluateTaskComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
