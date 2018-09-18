import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { MulticastCommandComponent } from './multicast-command.component';

describe('MulticastCommandComponent', () => {
  let component: MulticastCommandComponent;
  let fixture: ComponentFixture<MulticastCommandComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ MulticastCommandComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(MulticastCommandComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
