import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AsignarEvaluacionComponent } from './asignar-evaluacion.component';

describe('AsignarEvaluacionComponent', () => {
  let component: AsignarEvaluacionComponent;
  let fixture: ComponentFixture<AsignarEvaluacionComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ AsignarEvaluacionComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(AsignarEvaluacionComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
