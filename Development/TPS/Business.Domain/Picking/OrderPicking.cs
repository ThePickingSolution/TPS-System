﻿using Business.Domain.Events;
using Business.Domain.Exceptions;
using Business.Domain.People;
using Business.Domain.Validations;
using System;
using System.Collections.Generic;

namespace Business.Domain.Picking
{
    public class OrderPicking
    {
        private string _id;
        public string _container;
        private PickingStatus _status = PickingStatus.PENDING;
        private IOrderPickingValidator _validator = new DefaultOrderPickingValidator();
        private IOrderPickingEvent _event = new DefaultOrderPickingEvent();

        public string Id
        {
            get => _id;
            protected set
            {
                if (string.IsNullOrEmpty(value))
                    throw new InvalidOrderPickingIdException("Id vazio");
                _id = value;
            }
        }
        public string Description { get; set; }
        public bool WithContainer { get; set; }
        public string Container
        {
            get => _container;
            set
            {
                _validator.ValidateContainer(value,this);
                _container = value;
                _event.OnContainerChange(this);
            }
        }
        public string Area { get; protected set; }
        public Operator Operator { get; protected set; }
        public PickingStatus Status
        {
            get => _status;
            set
            {
                _validator.ValidateStatusChange(_status, value, this);
                PickingStatus previousAux = _status;
                _status = value;
                _event.OnStatusChange(this, previousAux);
            }
        }
        public IDictionary<string, string> Details { get; } = new Dictionary<string, string>();
        public ICollection<PickingItem> Items { get; protected set; } = new List<PickingItem>();
        
        public IOrderPickingValidator Validator {
            set {
                if (value == null)
                    throw new NullValidatorException("Order Picking Validator");
                _validator = value;
            } 
        }
        public IOrderPickingEvent Event
        {
            set
            {
                if (value == null)
                    throw new NullValidatorException("Null Order Picking Events");
                _event = value;
            }
        }

        public OrderPicking(string id) {
            this.Id = id;
        }
        public OrderPicking(string id,IOrderPickingValidator validator) {
            this.Id = id;
            this._validator = validator;
        }
        public OrderPicking(string id, IOrderPickingValidator validator, IOrderPickingEvent events)
        {
            this.Id = id;
            this._validator = validator;
            this._event = events;
        }
        public OrderPicking(string id, string container, string area, PickingStatus status, Operator _operator)
        {
            this.Id = id;
            this._container = container;
            this._status = status;
            this.Area = area;
            this.Operator = _operator;
        }


        //TODO Implement ordering
        public void PutItemsInOrder()
        {
            throw new NotImplementedException();
        }
        public void SetPickingOrderToUser(Operator _operator, string area)
        {
            this.Operator = _operator;
            this.Area = area;
            this.Status = PickingStatus.WIP;
        }
    
    }
}
