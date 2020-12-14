import { ComponentFixture, TestBed } from '@angular/core/testing';

import { HomeGrupoComponent } from './home-grupo.component';

describe('HomeGrupoComponent', () => {
  let component: HomeGrupoComponent;
  let fixture: ComponentFixture<HomeGrupoComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ HomeGrupoComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(HomeGrupoComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
