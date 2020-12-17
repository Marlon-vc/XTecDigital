import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SeeGroupNewsComponent } from './see-group-news.component';

describe('SeeGroupNewsComponent', () => {
  let component: SeeGroupNewsComponent;
  let fixture: ComponentFixture<SeeGroupNewsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ SeeGroupNewsComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(SeeGroupNewsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
