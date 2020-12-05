import { ComponentFixture, TestBed } from '@angular/core/testing';

import { GestionRubrosComponent } from './gestion-rubros.component';

describe('GestionRubrosComponent', () => {
  let component: GestionRubrosComponent;
  let fixture: ComponentFixture<GestionRubrosComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ GestionRubrosComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(GestionRubrosComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
