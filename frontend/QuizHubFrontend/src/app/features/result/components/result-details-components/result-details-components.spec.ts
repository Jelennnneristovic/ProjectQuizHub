import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ResultDetailsComponents } from './result-details-components';

describe('ResultDetailsComponents', () => {
  let component: ResultDetailsComponents;
  let fixture: ComponentFixture<ResultDetailsComponents>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [ResultDetailsComponents]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ResultDetailsComponents);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
